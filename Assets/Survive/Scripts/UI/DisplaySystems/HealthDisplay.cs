using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение здоровья игрока
/// </summary>
public class HealthDisplay : MonoBehaviour
{
    private HealthHandler healthManager;
    private Slider healthBar;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="healthManager">Класс, отвечающий за управление здоровьем</param>
    public void Init(
        HealthHandler healthManager, 
        Slider healthBar)
    {
        this.healthBar = healthBar;
        healthBar.minValue = 0;
        healthBar.maxValue = healthManager.GetMaxHealth();
        
        this.healthManager = healthManager;
        healthManager.OnDamageTaken += UpdateHealthBar;
        healthManager.OnHeal += UpdateHealthBar;

        UpdateHealthBar(healthManager.GetMaxHealth(), healthManager.GetMaxHealth());
    }

    /// <summary>
    /// Обновление очков здоровья в канвасе при получении урона
    /// </summary>
    /// <param name="currentHealth">Текущее здоровье</param>
    private void UpdateHealthBar(int currentHealth, int _, HealthComponent __, Vector3 ___)
    {
        healthBar.value = currentHealth;
    }

    /// <summary>
    /// Обновление очков здоровья в канвасе при лечении
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
            healthManager.OnDamageTaken -= UpdateHealthBar;
            healthManager.OnHeal -= UpdateHealthBar;
        }
    }
}
