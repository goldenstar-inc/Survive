using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение здоровья игрока
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    private HealthManager healthManager;
    private Slider healthBar;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="healthManager">Скрипт, отвечающий за управление здоровьем</param>
    public void Init(
        HealthManager healthManager, 
        Slider healthBar)
    {
        this.healthBar = healthBar;
        healthBar.minValue = 0;
        healthBar.maxValue = healthManager.GetMaxHealth();
        
        this.healthManager = healthManager;
        healthManager.OnTakeDamage += UpdateHealthBar;
        healthManager.OnHeal += UpdateHealthBar;

        UpdateHealthBar(healthManager.GetMaxHealth(), healthManager.GetMaxHealth());
    }

    /// <summary>
    /// Обновление очков здоровья в канвасе
    /// </summary>
    /// <param name="currentHealth">Текущее здоровье</param>
    /// <param name="maxHealth">Максимальное здоровье</param>
    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.value = currentHealth;
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
