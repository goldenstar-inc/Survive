using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ZombieAnimationController))]
[RequireComponent(typeof(HealthHandler))]
[RequireComponent(typeof(ZombieAttack))]
[RequireComponent(typeof(DropLoot))]
[RequireComponent(typeof(ZombieChase))]
public class ZombieManager : MonoBehaviour, IEnemy
{
    [SerializeField] Animator zombieAnimator;
    [SerializeField] LootPool poolData;
    [SerializeField] MovableZombieSetting setting;
    [SerializeField] ZombieAnimationController zombieAnimationController;
    [SerializeField] HealthHandler healthManager;
    [SerializeField] SoundController soundController;
    [SerializeField] ZombieAttack zombieAttack;
    [SerializeField] DropLoot dropLoot;
    [SerializeField] ZombieChase zombieChase;
    [SerializeField] KillDetector killDetector;

    public CreatureType creatureType { get; private set; }

    void Start()
    {
        int maxHealth = setting.HealthComponent.MaxHealth;
        int damage = setting.DamageComponent.Damage;
        int moveSpeed = setting.MovementComponent.WalkSpeed;
        float invincibleCooldown = setting.HealthComponent.InvincibilityCooldown;

        creatureType = setting.Type;

        AudioClip[] damageSound = setting.HealthComponent.DamagedSound;

        List<Loot> lootPool = poolData.Pool;

        if (maxHealth <= 0 || damage < 0 || moveSpeed <= 0 || invincibleCooldown < 0 ||  lootPool == null || lootPool.Count == 0)
        {
            Debug.LogError("Zombie properties aren't properly loaded");
            return;
        }

        healthManager.Init(
            maxHealth, 
            invincibleCooldown
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

        killDetector.Init(
            healthManager
            );
    }
}
