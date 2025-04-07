using System.Collections;
using System.Data.Common;
using System.Linq;
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
    private float attackRadius;
    private float attackCooldown;
    private float timeSinceLastAttack = 0f;
    private Transform shotStartPoint;
    private AudioClip[] swingSounds;

    public void Initialize(MeleeWeaponItemData data, PlayerDataProvider playerData)
    {
        this.playerData = playerData;

        this.data = data;
        damage = data.Damage;
        attackRadius = data.AttackRange;
        attackCooldown = data.AttackCooldown;
        swingSounds = data.SwingSounds; 
        Animator weaponAnimator = WeaponManager.Instance.GetWeaponAnimator();
        
        if (weaponAnimator != null)
        {
            weaponAnimator.runtimeAnimatorController = data.Animator;
        }
        shotStartPoint = WeaponManager.Instance.GetAttackStartPoint();
    }
    
    /// <summary>
    /// Вызываемый метод атаки ножом
    /// </summary>
    public bool Use()
    {
        if (Time.time - timeSinceLastAttack > attackCooldown)
        {
            Attack();
            return true;
        }   
        return false;
    }

    private void Attack()
    {
        WeaponManager.Instance.PlayAttackAnimation();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(shotStartPoint.position, attackRadius);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                HealthManager foundEnemy = enemy.GetComponent<HealthManager>();
                foundEnemy?.TakeDamage(damage);
            }
        }

        playerData.SoundController?.PlayRandomSound(swingSounds);
        timeSinceLastAttack = Time.time;
    }
}