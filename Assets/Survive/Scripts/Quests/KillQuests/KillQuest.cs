using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий квест, связанный с доставкой предметов
/// </summary>
[CreateAssetMenu(fileName = "KillQuest", menuName = "Quests/Kill Quest")]
public class KillQuest : Quest
{
    [SerializeField] private CreatureType questTargetType;
    public override event Action OnQuestCompleted;
    private int currentQuantity;
    private QuestManager questManager;
    private WeaponManager weaponManager;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questManager">Скрипт, управляющий квестами</param>
    /// <param name="inventoryController">Скрипт, управляющий инвентарем</param>
    public void Init(QuestManager questManager, WeaponManager weaponManager)
    {
        currentQuantity = 0;
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
        questManager.CompleteQuest();
        Dispose();
    }

    /// <summary>
    /// Уничтожение скрипта
    /// </summary>
    public override void Dispose()
    {
        if (weaponManager != null)
        {
            weaponManager.OnKill -= UpdateProgress;
        }
    }
}