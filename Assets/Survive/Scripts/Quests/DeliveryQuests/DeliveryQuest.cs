using System;
using UnityEngine;

/// <summary>
/// Квест доставки
/// </summary>
public class DeliveryQuest : IQuest, IDisposable
{
    public event Action OnCompleted;
    public QuestConfig questConfig;
    private int currentQuantity;
    private int maxProgress => questConfig.MaxProgress;
    private PickableItems questType;
    public QuestConfig QuestConfig => questConfig;
    private QuestManager questManager;
    private QuestEvents questEvents;
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questConfig">Конфиг квеста</param>
    public DeliveryQuest(DeliveryQuestConfig questConfig, PlayerDataProvider playerData, QuestEvents questEvents)
    {
        currentQuantity = 0;
        this.questConfig = questConfig;
        this.questEvents = questEvents;
        questType = questConfig.QuestItem;
        questEvents.OnItemPickedUp += UpdateProgress;

        if (playerData is IQuestProvider questProvider)
        {
            QuestManager questManager = questProvider.QuestManager;
            OnCompleted += questManager.CompleteQuest;
            this.questManager = questManager;
        }
    }

    public void UpdateProgress(int quantity, PickableItems itemType)
    {
        if (questType == itemType)
        {
            currentQuantity += quantity;
            questManager?.UpdateProgress(this, currentQuantity);
            if (currentQuantity >= maxProgress)
            {
                CompleteQuest(this);
            }
        }
    }

    /// <summary>
    /// Метод завершения квеста
    /// </summary>
    public void CompleteQuest(IQuest quest)
    {
        OnCompleted?.Invoke();
        Dispose();
    }

    public void Dispose()
    {
        OnCompleted = null;

        if (questEvents != null)
        {
            questEvents.OnItemPickedUp -= UpdateProgress;
        }
    }
}