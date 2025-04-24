using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    private HealthHandler healthManager;
    private List<Loot> lootPool;
    public void Init(List<Loot> lootPool, HealthHandler healthManager)
    {
        this.lootPool = lootPool;
        this.healthManager = healthManager;
        healthManager.OnDeath += Drop;
    }
    public void Drop()
    {
        if (lootPool == null)
        {
            Debug.LogError("LootPool is empty");
            return;
        }
        
        float chance = GetRandomChance();

        foreach (Loot loot in lootPool)
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

    private float GetRandomChance() 
    {
        return Random.Range(0, 100);
    }

    private void OnDisable()
    {
        if (healthManager != null)
        {
            healthManager.OnDeath -= Drop;
        }
    }
}
