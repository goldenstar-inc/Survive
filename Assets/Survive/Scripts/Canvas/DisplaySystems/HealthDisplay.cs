using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение здоровья игрока
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] HealthManager playerHealthManager;
    public Image[] hearts;

    /// <summary>
    /// Обновление очков здоровья в канвасе
    /// </summary>
    /// <param name="currentHealth">Текущее здоровье</param>
    /// <param name="maxHealth">Максимальное здоровье</param>
    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i].enabled = i < currentHealth;
        }
    }

    void OnEnable()
    {
        playerHealthManager.OnTakeDamage += UpdateHealthBar;
        playerHealthManager.OnHeal += UpdateHealthBar;
    }
    void OnDisable()
    {
        playerHealthManager.OnTakeDamage -= UpdateHealthBar;
        playerHealthManager.OnHeal -= UpdateHealthBar;
    }
}
