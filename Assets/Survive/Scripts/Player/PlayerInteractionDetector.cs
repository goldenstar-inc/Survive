using TMPro;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using static HelpPhrasesModule;

/// <summary>
/// Класс, отвечающий за управление детектором взаимодействий игрока
/// </summary>
public class PlayerInteractionDetector : MonoBehaviour
{
    public IPlayerDataProvider data { get; private set; }
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
    object interactableInRange = null;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
        helpPhrase.enabled = false;
        data = GetComponent<IPlayerDataProvider>();
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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

                if (interactable is IPickable pickable)
                {
                    interactableInRange = pickable;
                }
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
        if (interactableInRange != null && interactableInRange is IInteractable interactable)
        {
            if (interactable is IPickable pickable)
            {
                if (pickable.Data is InventoryItemData)
                {
                    if (UpdateInventory())
                    {
                        interactable.Interact(data);
                        SoundController.Instance.PlaySound(SoundType.PickUp, SoundController.Instance.inventoryAudioSource);
                    }
                    else
                    {
                        ShowInventoryFullTip();
                    }
                }
                else
                {
                    interactable.Interact(data);
                }
            }
        }
    }

    /// <summary>
    /// Подсказка: "Инвентарь заполнен"
    /// </summary>
    private void ShowInventoryFullTip() => helpPhrase.text = actionToPhrase[Action.InventoryFull];

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

    /// <summary>
    /// Метод, высвечивающий всплывающую подсказку для игрока
    /// </summary>
    private void ShowHelpPhrase()
    {
        if (helpPhrase != null && interactableInRange is IInteractable interactable)
        {
            helpPhrase.text = actionToPhrase[Action.PickUp];
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
