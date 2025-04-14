using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение здоровья игрока
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    private HealthManager healthManager;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="healthManager">Скрипт, отвечающий за управление здоровьем</param>
    public void Init(HealthManager healthManager)
    {
        this.healthManager = healthManager;
        healthManager.OnTakeDamage += UpdateHealthBar;
        healthManager.OnHeal += UpdateHealthBar;
        UpdateHealthBar(healthManager.GetCurrrentHealth(), healthManager.GetMaxHealth());
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
    void OnDisable()
    {
        if (healthManager != null)
        {
            healthManager.OnTakeDamage -= UpdateHealthBar;
            healthManager.OnHeal -= UpdateHealthBar;
        }
    }
}
