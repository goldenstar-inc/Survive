using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление детектором взаимодействий игрока
/// </summary>
public class PlayerInteractionDetector : MonoBehaviour
{
    public event Action<IInteractable> OnInteract;
    private PlayerDataProvider playerData;
    
    private InventoryController inventoryController;

    /// <summary>
    /// Текст, отображающий подсказки
    /// </summary>
    public TextMeshProUGUI helpPhrase;

    /// <summary>
    /// Ближайший объект для взаимодействия
    /// </summary>
    private List<IInteractable> interactablesInRange = new ();

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="playerData">Данные игрока</param>
    /// <param name="inventoryController">Контроллер игрока</param>
    public void Init(PlayerDataProvider playerData, InventoryController inventoryController)
    {
        this.playerData = playerData;
        this.inventoryController = inventoryController;
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
                interactablesInRange.Add(interactable); 
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
            if (collision.TryGetComponent(out IInteractable interactable))
            {
                interactablesInRange.Remove(interactable);
            }

            if (collision.TryGetComponent(out NPC _))
            {
                if (playerData is IDialogueProvider dialogueProvider)
                {
                    DialogueManager dialogueManager = dialogueProvider.DialogueManager;
                    dialogueManager.EndDialogue();
                }
            }
        }
    }

    /// <summary>
    /// Метод, отвечающий за попытку взаимодействия с объектом
    /// </summary>
    public void TryInteract()
    {
        if (interactablesInRange != null && interactablesInRange.Count > 0)
        {
            IInteractable interactable = interactablesInRange[0];

            if (interactable is IPickable pickable)
            {
                if (pickable.Data is InventoryItemData)
                {
                    if (UpdateInventory(pickable))
                    {
                        interactable.Interact(playerData);
                    }
                    else
                    {
                        // INVENTORY FULL TIP
                    }
                }
                else
                {
                    if (interactable.Interact(playerData))
                    {
                        OnInteract?.Invoke(interactable);
                    }
                }
            }
            else
            {
                if (interactable.Interact(playerData))
                {
                    OnInteract?.Invoke(interactable);
                }
            }
        }
    }

    /// <summary>
    /// Метод, обновляющий инвентарь
    /// </summary>
    /// <returns>True - если предмет был успешно подобран и передан в инвентарь, иначе - false</returns>
    private bool UpdateInventory(IPickable pickable)
    {
        if (inventoryController != null)
        {
            return inventoryController.AddItemToInventory(pickable);
        }

        return false;
    }
}
