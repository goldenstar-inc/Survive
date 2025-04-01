using System;
using UnityEngine;

public class EquippedHealableItem : IUseScript
{
    private HealthManager healthManager;
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

    private void ApplyHeal()
    {
        if (healthManager != null)
        {
            healthManager.Heal(healPoints);
        }
    }
}
