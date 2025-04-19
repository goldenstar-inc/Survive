using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий квест, связанный с доставкой предметов
/// </summary>
public class KillQuest : IQuest, IDisposable
{
    public KillQuestConfig questConfig;

    private int currentQuantity;
    private QuestManager questManager;
    private WeaponManager weaponManager;
    public event Action OnCompleted;

    private CreatureType questTargetType => questConfig.QuestTargetType;
    private int maxProgress => questConfig.MaxProgress;
    public QuestConfig QuestConfig => questConfig;


    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questManager">Скрипт, управляющий квестами</param>
    /// <param name="weaponManager">Скрипт, управляющий атакой</param>
    public void Init(KillQuestConfig questConfig, QuestManager questManager, WeaponManager weaponManager)
    {
        currentQuantity = 0;
        this.questConfig = questConfig;
        this.questManager = questManager;
        this.weaponManager = weaponManager;
        this.weaponManager.OnKill += UpdateProgress;
    }

    /// <summary>
    /// Обновление прогресса
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <param name="quantity">Количество подобранного предмета</param>
    /// <param name="data">Данные о подобранном предмете</param>
    private void UpdateProgress(CreatureType enemyType)
    {
        if (questTargetType == enemyType)
        {
            currentQuantity += 1;
            questManager?.UpdateProgress(this, currentQuantity);
        }

        if (currentQuantity == maxProgress)
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
        questManager.CompleteQuest();
        Dispose();
    }

    /// <summary>
    /// Уничтожение скрипта
    /// </summary>
    public void Dispose()
    {
        if (weaponManager != null)
        {
            weaponManager.OnKill -= UpdateProgress;
        }
    }
}