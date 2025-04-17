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

    PlayerDataProvider playerData;

    public void Initialize(int damage, PlayerDataProvider playerData)
    {
        this.damage = damage;

        this.playerData = playerData;
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
                KillDetector killDetector = collision.GetComponent<KillDetector>();

                killDetector?.SetPlayerData(playerData);
                damageHandler?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
