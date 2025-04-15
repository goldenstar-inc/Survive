using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий квест, связанный с доставкой предметов
/// </summary>
[CreateAssetMenu(fileName = "DeliveryQuest", menuName = "Quests/Delivery Quest")]
public class DeliveryQuest : Quest
{
    [SerializeField] private PickableItems questItem;
    public override event Action OnQuestCompleted;
    private int currentQuantity;
    private QuestManager questManager;
    private InventoryController inventoryController;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questManager">Скрипт, управляющий квестами</param>
    /// <param name="inventoryController">Скрипт, управляющий инвентарем</param>
    public void Init(QuestManager questManager, InventoryController inventoryController)
    {
        currentQuantity = 0;
        this.questManager = questManager;
        this.inventoryController = inventoryController;
        this.inventoryController.OnPickUp += UpdateProgress;
    }

    /// <summary>
    /// Обновление прогресса
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <param name="quantity">Количество подобранного предмета</param>
    /// <param name="data">Данные о подобранном предмете</param>
    private void UpdateProgress(int index, int quantity, InventoryItemData data)
    {
        // [TO FIX]
        quantity = 1;

        if (questItem == data.Name)
        {
            currentQuantity += quantity;
            questManager?.UpdateProgress(this, currentQuantity);
        }

        if (currentQuantity == MaxProgress)
        {
            CompleteQuest();
        }
    }

    /// <summary>
    /// Метод завершения квеста
    /// </summary>
    private void CompleteQuest()
    {
        OnQuestCompleted?.Invoke();
        questManager.CompeteQuest();
        Dispose();
    }

    /// <summary>
    /// Уничтожение скрипта
    /// </summary>
    public override void Dispose()
    {
        if (inventoryController != null)
        {
            inventoryController.OnPickUp -= UpdateProgress;
        }
    }
}