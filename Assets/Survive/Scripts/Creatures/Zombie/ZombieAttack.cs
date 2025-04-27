using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Класс, отвечающий за атаку зомби
/// </summary>
public class ZombieAttack : MonoBehaviour
{
    private ZombieChase zombieChase;
    private int damage { get; set; }

    /// <summary>
    /// Инициализация скрипта [DI]
    /// </summary>
    /// <param name="damage">Урон</param>
    public void Init(int damage, ZombieChase zombieChase)
    {
        this.damage = damage;
        this.zombieChase = zombieChase;
        zombieChase.OnCaughtTarget += Attack;
    }

    /// <summary>
    /// Атака зомби
    /// </summary>
    /// <param name="targetHealthManager">Скрипт цели, отвечающий за здоровье</param>
    public void Attack(HealthHandler targetHealthManager)
    {
        if (targetHealthManager != null)
        {
            targetHealthManager.TakeDamage(damage);
            if (targetHealthManager.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(new Vector2(0, 0.2f), ForceMode2D.Force);
            }
        }
    }

    void OnDisable()
    {
        zombieChase.OnCaughtTarget -= Attack;
    }
}
