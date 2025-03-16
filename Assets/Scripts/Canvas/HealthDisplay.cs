using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение здоровья игрока
/// </summary>
public class HealthDisplay : MonoBehaviour, IDamageObserver, IHealObserver
{
    public Image[] hearts;

    /// <summary>
    /// Метод, вызывающийся при получении урона игроком
    /// </summary>
    /// <param name="currentHealth">Текущее количество очков здоровья</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    public void OnDamageTaken(int currentHealth, int maxHealth)
    {
        UpdateHealthBar(currentHealth, maxHealth);
    }

    /// <summary>
    /// Метод, вызывающийся при восстановлении очков здоровья игроком
    /// </summary>
    /// <param name="currentHealth">Текущее количество очков здоровья</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    public void OnHealApplied(int currentHealth, int maxHealth)
    {
        UpdateHealthBar(currentHealth, maxHealth);
    }

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
}
