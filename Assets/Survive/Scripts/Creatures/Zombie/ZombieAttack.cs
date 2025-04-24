using Unity.VisualScripting;
using UnityEngine;

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
        }
    }

    void OnDisable()
    {
        zombieChase.OnCaughtTarget -= Attack;
    }
}
