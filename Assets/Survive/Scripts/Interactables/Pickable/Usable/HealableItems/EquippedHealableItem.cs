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
    private IPlayerDataProvider playerData;

    /// <summary>
    /// Количество очков здоровья для лечения
    /// </summary>
    private int healPoints;
    private SoundType useSound;
    public void Initialize(HealableItemData data, IPlayerDataProvider playerData)
    {
        this.playerData = playerData;
        healPoints = data.HealPoints;
        useSound = data.Sound;
    }

    public bool Use()
    {
        if (playerData != null)
        {
            HealthManager healthManager = playerData.HealthManager;

            if (healthManager != null)
            {
                ApplyHeal(healthManager);
                playerData.SoundController?.PlaySound(useSound);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Метод лечения
    /// </summary>
    private void ApplyHeal(HealthManager healthManager)
    {
        if (healthManager != null)
        {
            healthManager.Heal(healPoints);
        }
    }
}
