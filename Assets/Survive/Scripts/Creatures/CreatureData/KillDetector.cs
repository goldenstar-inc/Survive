using System.Collections.Generic;
using UnityEngine;

public class KillDetector : MonoBehaviour
{
    private HealthHandler healthManager;
    private PlayerDataProvider playerData;

    public void Init(HealthHandler healthManager)
    {
        this.healthManager = healthManager;
        healthManager.OnDeath += KillDetected;
    }

    public void SetPlayerData(PlayerDataProvider playerData)
    {
        this.playerData = playerData;
    }

    private void KillDetected()
    {
        if (TryGetComponent(out IEnemy enemy))
        {
            playerData?.WeaponManager?.Kill(enemy.creatureType, healthManager);
        }
    }

    private void OnDisable()
    {
        if (healthManager != null)
        {
            healthManager.OnDeath -= KillDetected;
        }
    }
}
