using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс, реализующий механику пули
/// </summary>
public class Bullet : MonoBehaviour
{
    private int damage;
    private PlayerDataProvider playerData;

    public void Initialize(
        int damage, 
        PlayerDataProvider playerData
        )
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
                HealthHandler damageHandler = collision.GetComponent<HealthHandler>();
                KillDetector killDetector = collision.GetComponent<KillDetector>();

                if (playerData is IHealthProvider healthProvider)
                {
                    HealthHandler healthHandler = healthProvider.HealthHandler;
                    damageHandler?.TakeDamage(damage, healthHandler.transform.position);
                }
                killDetector?.SetPlayerData(playerData);
                Destroy(gameObject);
            }
        }
    }
}
