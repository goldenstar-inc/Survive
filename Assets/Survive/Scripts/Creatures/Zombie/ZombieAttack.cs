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
    /// Инициализация
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
    /// <param name="targetHealthManager">Класс цели, отвечающий за здоровье</param>
    public void Attack(HealthHandler targetHealthManager)
    {
        if (targetHealthManager != null)
        {
            targetHealthManager.TakeDamage(damage, transform.position);
        }
    }

    void OnDisable()
    {
        zombieChase.OnCaughtTarget -= Attack;
    }
}
