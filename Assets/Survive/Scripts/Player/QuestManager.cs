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
    public event Action<Quest> OnQuestCompleted;
    private Quest currentQuest;
    private InventoryController inventoryController;
    private WeaponManager weaponManager;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="inventoryController">Скрипт, управляющий инвентарем</param>
    /// <param name="weaponManager">Скрипт, управляющий оружием</param>
    public void Init(InventoryController inventoryController, WeaponManager weaponManager)
    {
        this.inventoryController = inventoryController;
        this.weaponManager = weaponManager;
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
        else if (quest is ExplorationQuest explorationQuest)
        {
            explorationQuest.Init(this);
            currentQuest = explorationQuest;
            OnQuestAdded?.Invoke(currentQuest);
        }
        else if (quest is KillQuest killQuest)
        {
            killQuest.Init(this, weaponManager);
            currentQuest = killQuest;
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
    public void CompleteQuest()
    {
        OnQuestCompleted?.Invoke(currentQuest);
        currentQuest = null;
    }

    /// <summary>
    /// Получение текущего квеста
    /// </summary>
    /// <returns>Текущий квест</returns>
    public Quest GetCurrentQuest()
    {
        return currentQuest;
    }
}
