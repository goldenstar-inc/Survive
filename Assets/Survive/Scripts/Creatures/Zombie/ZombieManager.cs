using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UIElements;

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
    [SerializeField] HealthHandler healthHandler;
    [SerializeField] SoundController soundController;
    [SerializeField] ZombieAttack zombieAttack;
    [SerializeField] DropLoot dropLoot;
    [SerializeField] ZombieChase zombieChase;
    [SerializeField] KillDetector killDetector;
    [SerializeField] StateHandler stateHandler;
    [SerializeField] KnockbackHandler knockbackHandler;
    [SerializeField] Rigidbody2D rb;
    public CreatureType creatureType { get; private set; }

    void Start()
    {
        int maxHealth = setting.HealthComponent.MaxHealth;
        int damage = setting.DamageComponent.Damage;
        int moveSpeed = setting.MovementComponent.WalkSpeed;
        float invincibleCooldown = setting.HealthComponent.InvincibilityCooldown;

        var healthComponent = setting.HealthComponent;
        
        creatureType = setting.Type;

        AudioClip[] damageSound = setting.HealthComponent.DamagedSounds;

        List<Loot> lootPool = poolData.Pool;

        if (maxHealth <= 0 || damage < 0 || moveSpeed <= 0 || invincibleCooldown < 0 ||  lootPool == null || lootPool.Count == 0)
        {
            Debug.LogError("Zombie properties aren't properly loaded");
            return;
        }

        stateHandler.Init();

        knockbackHandler.Init(
            rb,
            stateHandler,
            healthHandler
        );

        healthHandler.Init(
            healthComponent,
            maxHealth, 
            invincibleCooldown
            );

        dropLoot.Init(
            lootPool, 
            healthHandler
            );

        zombieAnimationController.Init(
            healthHandler, 
            zombieAnimator
            );
        
        zombieChase.Init(
            moveSpeed, 
            rb,
            zombieAnimationController,
            stateHandler
            );

        zombieAttack.Init(
            damage, 
            zombieChase
            );

        killDetector.Init(
            healthHandler
            );
    }
}
