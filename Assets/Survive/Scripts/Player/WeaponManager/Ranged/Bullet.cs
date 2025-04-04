using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс, реализующий механику пули
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Урон пули пистолета
    /// </summary>
    private int damage;

    public void Initialize(int damage)
    {
        this.damage = damage;
    }

    /// <summary>
    /// Метод, срабатывающий при входе в триггер 
    /// </summary>
    /// <param name="collision">Объект коллизии</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                HealthManager damageHandler = collision.GetComponent<HealthManager>();

                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
