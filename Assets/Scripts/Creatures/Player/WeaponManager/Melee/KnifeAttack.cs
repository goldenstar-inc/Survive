using System.Collections;
using System.Data.Common;
using UnityEngine;
using static InventoryController;

/// <summary>
/// Класс отвечающий за атаку ножом
/// </summary>
public class KnifeAttack : MonoBehaviour, IAttackScript
{
    /// <summary>
    /// Конфиг оружия
    /// </summary>
    [SerializeField] public WeaponItemData data;
    [SerializeField] public WeaponAnimator animator;

    /// <summary>
    /// Коллайдер атаки
    /// </summary>
    public CircleCollider2D attackCollider;

    /// <summary>
    /// Урон с ножа
    /// </summary>
    private int damage => data.Damage;

    /// <summary>
    /// Скорость атаки
    /// </summary>
    private float attackCooldown => data.AttackCooldown;

    /// <summary>
    /// Найденный враг для нанесения урона
    /// </summary>
    private HealthManager foundEnemy;

    /// <summary>
    /// Вызываемый метод атаки ножом
    /// </summary>
    public void Attack()
    {
        StartCoroutine(AttackEnumerator());
    }

    /// <summary>
    /// Метод, отвечающий за корректное прохождение урона
    /// </summary>
    private IEnumerator AttackEnumerator()
    {
        EnableCollider();
        animator?.PlayAttackAnimation();
        yield return new WaitForSeconds(attackCooldown);
        DisableCollider();
    }

    /// <summary>
    /// Включение коллайдера ножа
    /// </summary>
    private void EnableCollider() => attackCollider.enabled = true;

    /// <summary>
    /// Выключение коллайдера ножа
    /// </summary>
    private void DisableCollider() => attackCollider.enabled = false;

    /// <summary>
    /// Функция, применяемая при контакте с триггером
    /// </summary>
    /// <param name="collision">Коллизия триггера</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                foundEnemy = collision.GetComponent<HealthManager>();
                foundEnemy?.TakeDamage(damage);
            }
        }
    }
}