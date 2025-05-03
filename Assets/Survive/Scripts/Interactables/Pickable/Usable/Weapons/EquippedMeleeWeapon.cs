using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static InventoryController;

/// <summary>
/// Класс отвечающий за атаку ножом
/// </summary>
public class EquippedMeleeWeapon : IUseScript
{
    /// <summary>
    /// Информация о взаимодействующем персонаже
    /// </summary>
    private PlayerDataProvider playerData;

    /// <summary>
    /// Конфиг оружия
    /// </summary>
    private MeleeWeaponItemData data;
    private int damage;
    private Collider2D attackAreaCollider;
    private float attackCooldown;
    private float timeSinceLastAttack = 0f;
    private Transform shotStartPoint;
    private WeaponManager weaponManager;
    public void Init(MeleeWeaponItemData data, PlayerDataProvider playerData)
    {
        this.playerData = playerData;
        weaponManager = playerData?.WeaponManager;
        shotStartPoint = weaponManager.GetAttackStartPoint();
        Animator weaponAnimator = weaponManager.GetWeaponAnimator();
        if (weaponAnimator.runtimeAnimatorController != null)
        {
            weaponAnimator.runtimeAnimatorController = data.Animator;
        }
        
        this.data = data;
        damage = data.Damage;
        attackCooldown = data.AttackCooldown;

        GameObject attackArea = data.AttackArea;
        if (attackArea != null)
        {
            GameObject spawnedArea = Spawner.Instance.Spawn(attackArea, shotStartPoint.transform.position, Quaternion.identity);
            spawnedArea.transform.SetParent(weaponManager.transform);
            attackAreaCollider = spawnedArea.GetComponentInChildren<Collider2D>();
            attackAreaCollider.enabled = false;
        }
    }
    
    /// <summary>
    /// Вызываемый метод атаки ножом
    /// </summary>
    public bool Use()
    {
        if (Time.time - weaponManager.GetTimeSinceLastAttack() > data.AttackCooldown)
        {
            Attack();
            return true;
        }
        return false;
    }

    private void Attack()
    {
        weaponManager.Attack(data);

        if (attackAreaCollider == null)
        {
            Debug.LogError("Attack area collider missing!");
            return;
        }

        attackAreaCollider.enabled = true;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Enemy"));
        filter.useTriggers = true;

        Collider2D[] hitEnemies = new Collider2D[10];

        int hitEnemiesCount = attackAreaCollider.Overlap(filter, hitEnemies);

        for (int i = 0; i < hitEnemiesCount; i++)
        {
            Collider2D enemy = hitEnemies[i];
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                HealthHandler foundEnemy = enemy.GetComponent<HealthHandler>();
                KillDetector killDetector = foundEnemy.GetComponent<KillDetector>();

                if (playerData is IHealthProvider healthProvider)
                {
                    HealthHandler healthHandler = healthProvider.HealthHandler;
                    foundEnemy?.TakeDamage(damage, healthHandler.transform.position);
                }
                killDetector?.SetPlayerData(playerData);
            }
        }        

        attackAreaCollider.enabled = false;
        timeSinceLastAttack = Time.time;
    }
}