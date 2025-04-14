using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ZombieAnimationController))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(ZombieAttack))]
[RequireComponent(typeof(DropLoot))]
[RequireComponent(typeof(ZombieChase))]
public class ZombieManager : MonoBehaviour
{
    [SerializeField] Animator zombieAnimator;
    [SerializeField] LootPool poolData;
    [SerializeField] MovableZombieSetting setting;
    [SerializeField] ZombieAnimationController zombieAnimationController;
    [SerializeField] HealthManager healthManager;
    [SerializeField] SoundController soundController;
    [SerializeField] ZombieAttack zombieAttack;
    [SerializeField] DropLoot dropLoot;
    [SerializeField] ZombieChase zombieChase;

    void Start()
    {
        int maxHealth = setting.HealthComponent.MaxHealth;
        int damage = setting.DamageComponent.Damage;
        int moveSpeed = setting.MovementComponent.WalkSpeed;
        float invincibleCooldown = setting.HealthComponent.InvincibilityCooldown;

        AudioClip damageSound = setting.HealthComponent.DamageSound;

        List<Loot> lootPool = poolData.Pool;

        if (maxHealth <= 0 || damage < 0 || moveSpeed <= 0 || invincibleCooldown < 0 ||  lootPool == null || lootPool.Count == 0)
        {
            Debug.LogError("Zombie properties aren't properly loaded");
            return;
        }

        healthManager.Init(
            maxHealth, 
            damageSound, 
            invincibleCooldown,
            soundController 
            );

        dropLoot.Init(
            lootPool, 
            healthManager
            );

        zombieAnimationController.Init(
            healthManager, 
            zombieAnimator
            );
        
        zombieChase.Init(
            moveSpeed, 
            zombieAnimationController
            );

        zombieAttack.Init(
            damage, 
            zombieChase
            );
    }
}
