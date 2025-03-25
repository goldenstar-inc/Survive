using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс, реализующий механику пули
/// </summary>
public class PistolBullet : MonoBehaviour
{
    /// <summary>
    /// Урон пули пистолета
    /// </summary>
    public int damage => Pistol.damage;

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
                DamageHandler damageHandler = collision.GetComponent<DamageHandler>();

                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
