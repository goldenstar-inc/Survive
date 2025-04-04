using System.Collections.Generic;
using UnityEngine;

public class BigZombieInitialization : MonoBehaviour
{
    /// <summary>
    /// Скрипт, отвечающий за управление здоровьем большого зомби
    /// </summary>
    private HealthManager healthManager;

    public ZombieAnimationController zombieAnimationController;
    
    /// <summary>
    /// Максимальное здоровье большого зомби
    /// </summary>
    public int maxHealth { get; set; }
    void Start()
    {
        healthManager = GetComponent<HealthManager>();

        if (healthManager != null)
        {
            maxHealth = 100;
        }
    }
}
