using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий скрипт лечащих предметов
/// </summary>
public class EquippedHealableItem : IUseScript
{
    /// <summary>
    /// Скрипт, управляющий здоровьем
    /// </summary>
    private PlayerDataProvider playerData;

    /// <summary>
    /// Количество очков здоровья для лечения
    /// </summary>
    private int healPoints;
    public void Init(HealableItemData data, PlayerDataProvider playerData)
    {
        this.playerData = playerData;
        healPoints = data.HealPoints;
    }

    public bool Use()
    {
        if (playerData != null)
        {
            HealthHandler healthManager = playerData.HealthHandler;

            if (healthManager != null)
            {
                if (!healthManager.IsFullHealth())
                {
                    ApplyHeal(healthManager);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Метод лечения
    /// </summary>
    private void ApplyHeal(HealthHandler healthManager)
    {
        healthManager?.Heal(healPoints);
    }
}
