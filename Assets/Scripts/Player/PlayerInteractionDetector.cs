using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


/// <summary>
/// Класс, отвечающий за управление детектором взаимодействий игрока
/// </summary>
public class PlayerInteractionDetector : MonoBehaviour
{
    public InventoryController inventoryController;

    /// <summary>
    /// Компонент "Transform" игрока
    /// </summary>
    public Transform player;

    /// <summary>
    /// Текст, отображающий подсказки
    /// </summary>
    public TextMeshProUGUI helpPhrase;

    /// <summary>
    /// Ближайший объект для взаимодействия
    /// </summary>
    IInteractable interactableInRange = null;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        helpPhrase.enabled = false;
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        if (interactableInRange != null)
        {
            UpdateHelpPhrasePosition();
        }
    }

    /// <summary>
    /// Метод, вызывающийся при триггере коллайдера
    /// </summary>
    /// <param name="collision">Коллизия с объектом</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.TryGetComponent(out IInteractable interactable))
            {
                interactableInRange = interactable; 
                ShowHelpPhrase();
            }
        }
    }

    /// <summary>
    /// Метод, вызывающийся при выходе из триггера с коллайдером
    /// </summary>
    /// <param name="collision">Коллизия с объектом</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            interactableInRange = null;
            helpPhrase.enabled = false;
        }
    }

    /// <summary>
    /// Метод, отвечающий за попытку взаимодействия с объектом
    /// </summary>
    private void TryInteract()
    {
        if (interactableInRange != null)
        {
            interactableInRange.Interact();
            inventoryController.AddItemToInventory(interactableInRange.Ge);
        }
    }

    /// <summary>
    /// Метод, высвечивающий всплывающую подсказку для игрока
    /// </summary>
    private void ShowHelpPhrase()
    {
        if (helpPhrase != null)
        {
            helpPhrase.text = interactableInRange.GetHelpPhrase();
            helpPhrase.enabled = true;
            
        }
    }

    /// <summary>
    /// Обновление позиции подсказки
    /// </summary>
    private void UpdateHelpPhrasePosition()
    {
        if (player != null && helpPhrase != null)
        {
            float distanceAbovePlayer = 1f;
            Vector3 positionAbovePlayer = player.position + new Vector3(0, distanceAbovePlayer, 0);
            helpPhrase.transform.position = positionAbovePlayer;
        }
    }
}
