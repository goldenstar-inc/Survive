using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BigZombieInitialization : MonoBehaviour
{
    /// <summary>
    /// Скрипт, отвечающий за управление здоровьем большого зомби
    /// </summary>
    private HealthManager healthManager;
    public ZombieAnimationController zombieAnimationController;
    [SerializeField] LootPool poolData;
    private List<Loot> pool => poolData.Pool;
    
    /// <summary>
    /// Максимальное здоровье большого зомби
    /// </summary>
    public int maxHealth { get; set; }
    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.OnDeath += DropItem;
        if (healthManager != null)
        {
            maxHealth = 100;
        }
    }

    public void DropItem()
    {
        float chance = Random.Range(0, 100);

        foreach (Loot loot in pool)
        {
            if (loot.DropChance >= chance)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-1f, 1f), 
                    0f, 
                    Random.Range(-1f, 1f)
                ).normalized * Random.Range(0.5f, 1.5f);

                Vector3 spawnPosition = transform.position + randomOffset;
                Instantiate(loot.ItemPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
