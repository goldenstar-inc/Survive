using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static InventoryController;

/// <summary>
/// Класс, ответственный за восстановление очков здоровья игрока
/// </summary>
public class PlayerHeal : MonoBehaviour
{
    /// <summary>
    /// Скрипт, отвечающий за здоровье игрока
    /// </summary>
    private HealHandler healHandler;

    /// <summary>
    /// Количество очков, на которое аптечка восполняет здоровье
    /// </summary>
    private int healPoints = 1;

    /// <summary>
    /// Скрипт, хранящий информацию об инвентаре игрока
    /// </summary>
    private InventoryController inventoryController;
    
    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        healHandler = GetComponent<HealHandler>();
        inventoryController = GetComponent<InventoryController>();

        if (healHandler == null)
        {
            Debug.LogWarning("HealHandler not loaded");
        }

        if (inventoryController != null)
        {
            Debug.LogWarning("InventoryController not loaded");
        }
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && healHandler != null)
        {
            if (inventoryController.GetCurrentPickableItem() != null)
            {
                if (inventoryController.GetCurrentPickableItem().UniqueName == PickableItems.Medkit)
                {
                    if (healHandler.Heal(healPoints))
                    {
                        inventoryController.RemoveElementFromInventory();
                        SoundController.Instance.PlaySound(SoundType.BeingHealed, SoundController.Instance.inventoryAudioSource);
                    }
                }
            }
        }
    }
}
