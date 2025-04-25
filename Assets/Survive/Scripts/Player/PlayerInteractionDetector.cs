using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление детектором взаимодействий игрока
/// </summary>
public class PlayerInteractionDetector : MonoBehaviour
{
    public event Action<IPickable> OnPickUp;
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
    private IInteractable interactableInRange = null;

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
                interactableInRange = interactable; 
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
            if (collision.TryGetComponent(out NPC _))
            {
                if (playerData is IDialogueProvider dialogueProvider)
                {
                    DialogueManager dialogueManager = dialogueProvider.DialogueManager;
                    dialogueManager.EndDialogue();
                }
            }
            interactableInRange = null;
        }
    }

    /// <summary>
    /// Метод, отвечающий за попытку взаимодействия с объектом
    /// </summary>
    public void TryInteract()
    {
        if (interactableInRange != null && interactableInRange is IInteractable interactable)
        {
            if (interactable is IPickable pickable)
            {
                if (pickable.Data is InventoryItemData)
                {
                    if (UpdateInventory())
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
                    interactable.Interact(playerData);
                    OnPickUp?.Invoke(pickable);
                }
            }
            else
            {
                interactable.Interact(playerData);
                OnInteract?.Invoke(interactable);
            }
        }
    }

    /// <summary>
    /// Метод, обновляющий инвентарь
    /// </summary>
    /// <returns>True - если предмет был успешно подобран и передан в инвентарь, иначе - false</returns>
    private bool UpdateInventory()
    {
        if (inventoryController != null && interactableInRange != null && interactableInRange is IPickable pickable)
        {
            return inventoryController.AddItemToInventory(pickable);
        }

        return false;
    }
}
