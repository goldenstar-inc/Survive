using System.Collections.Generic;
using UnityEngine;

public class KillDetector : MonoBehaviour
{
    private HealthManager healthManager;
    private PlayerDataProvider playerData;

    public void Init(HealthManager healthManager)
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
        if (TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            playerData?.WeaponManager?.Kill(enemy.creatureType);
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
