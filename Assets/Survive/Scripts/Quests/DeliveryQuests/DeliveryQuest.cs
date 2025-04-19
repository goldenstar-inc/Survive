using System;
using UnityEngine;

/// <summary>
/// Квест доставки
/// </summary>
public class DeliveryQuest : IQuest
{
    public event Action OnCompleted;
    public QuestConfig questConfig;
    private int currentQuantity;
    private int maxProgress => questConfig.MaxProgress;
    public QuestConfig QuestConfig => questConfig;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questConfig">Конфиг квеста</param>
    public DeliveryQuest(DeliveryQuestConfig questConfig)
    {
        this.questConfig = questConfig;
        currentQuantity = 0;
    }

    public void AddProgress(int quantity)
    {
        currentQuantity += quantity;

        if (currentQuantity >= maxProgress)
        {
            CompleteQuest();
        }
    }

    /// <summary>
    /// Метод завершения квеста
    /// </summary>
    public void CompleteQuest()
    {
        OnCompleted?.Invoke();
    }
}