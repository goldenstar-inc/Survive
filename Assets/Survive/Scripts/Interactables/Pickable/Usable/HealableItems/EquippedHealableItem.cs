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
    private HealthManager healthManager;

    /// <summary>
    /// Количество очков здоровья для лечения
    /// </summary>
    private int healPoints;
    private SoundType sound;
    public void Initialize(HealableItemData data, HealthManager healthManager)
    {
        this.healthManager = healthManager;
        healPoints = data.HealPoints;
        sound = data.Sound;
    }

    public bool Use()
    {
        if (healthManager != null && healthManager.currentHealth != healthManager.maxHealth)
        {
            ApplyHeal();
            SoundController.Instance.PlaySound(sound, SoundController.Instance.inventoryAudioSource);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Метод лечения
    /// </summary>
    private void ApplyHeal()
    {
        if (healthManager != null)
        {
            healthManager.Heal(healPoints);
        }
    }
}
