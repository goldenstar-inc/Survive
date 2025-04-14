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
    private AudioClip useSound;
    public void Init(HealableItemData data, PlayerDataProvider playerData)
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
                if (!healthManager.IsFullHealth())
                {
                    ApplyHeal(healthManager);
                    playerData.SoundController?.PlayAudioClip(useSound);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Метод лечения
    /// </summary>
    private void ApplyHeal(HealthManager healthManager)
    {
        healthManager?.Heal(healPoints);
    }
}
