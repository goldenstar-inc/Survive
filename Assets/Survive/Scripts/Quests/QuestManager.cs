using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Класс, управляющий квестами
/// </summary>
public class QuestManager : MonoBehaviour
{
    public event Action<Quest> OnQuestAdded;
    public event Action<Quest, int> OnProgressUpdated;
    public event Action OnQuestCompleted;
    private Quest currentQuest;
    private InventoryController inventoryController;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="inventoryController">Скрипт, управляющий инвентарем</param>
    public void Init(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
    }

    /// <summary>
    /// Метод добавления нового квеста
    /// </summary>
    /// <param name="quest">Новый квест</param>
    public void AddQuest(Quest quest)
    {
        if (quest is DeliveryQuest deliveryQuest)
        {
            deliveryQuest.Init(this, inventoryController);
            currentQuest = deliveryQuest;
            OnQuestAdded?.Invoke(currentQuest);
        }
    }

    /// <summary>
    /// Метод, зажигающий событие при обновлении прогресса
    /// </summary>
    /// <param name="quest">Квест</param>
    /// <param name="currentProgress">Текущий прогресс</param>
    public void UpdateProgress(Quest quest, int currentProgress)
    {
        OnProgressUpdated?.Invoke(quest, currentProgress);
    }

    /// <summary>
    /// Метод, зажигающий событие при завершении квеста
    /// </summary>
    public void CompeteQuest()
    {
        OnQuestCompleted?.Invoke();
    }
}
