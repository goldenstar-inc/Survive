using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за атаку зомби
/// </summary>
public class ZombieAttack : MonoBehaviour
{
    public int damage { get; private set; }

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        damage = 1;
    }

    /// <summary>
    /// Метод, запускающийся при триггере объекта зомби
    /// </summary>
    /// <param name="collision">Объект коллизии</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) 
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HealthManager damageHandler = collision.GetComponent<HealthManager>();
                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(damage);
                }
            }
        }
    }
}
