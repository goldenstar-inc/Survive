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
    private Transform shotStartPoint;
    public void Initialize(MeleeWeaponItemData data)
    {
        this.data = data;
        damage = data.Damage;
        attackRadius = data.AttackRange;
        shotStartPoint = WeaponManager.Instance.GetAttackStartPoint();
    }
    
    /// <summary>
    /// Вызываемый метод атаки ножом
    /// </summary>
    public bool Use()
    {
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
        return true;
    }
}