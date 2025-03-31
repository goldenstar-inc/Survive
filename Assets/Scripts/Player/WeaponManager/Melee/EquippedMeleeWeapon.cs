using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using static InventoryController;
/// <summary>
/// Класс отвечающий за атаку ножом
/// </summary>
public class EquippedMeleeWeapon : IUseScript
{
    /// <summary>
    /// Конфиг оружия
    /// </summary>
    private MeleeWeaponItemData data;
    private int damage;
    private float attackRadius;
    private float attackCooldown;
    private float timeSinceLastAttack = 0f;
    private Transform shotStartPoint;
    public void Initialize(MeleeWeaponItemData data)
    {
        this.data = data;
        damage = data.Damage;
        attackRadius = data.AttackRange;
        attackCooldown = data.AttackCooldown;

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
        }   

        return true;
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
        SoundController.Instance.PlayRandomSwingingKnifeSound();
        timeSinceLastAttack = Time.time;
    }
}