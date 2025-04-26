using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public event Action<int, int, HealthComponent> OnDamageTaken;
    public event Action<int, int> OnHeal;
    public event Action OnDeath;

    private HealthComponent healthComponent;
    private int currentHealth;
    private int maxHealth;
    private float invincibilityCooldown;
    private float timeSinceLastDamageTaken = 0f;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="healthComponent">Компонент, хранащий информацию, связанную со здоровьем</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    /// <param name="invincibilityCooldown">Время иммунитета после получения урона</param>
    public void Init(
        HealthComponent healthComponent,
        int maxHealth, 
        float invincibilityCooldown
        ) 
    {
        this.healthComponent = healthComponent;
        this.maxHealth = maxHealth;
        this.invincibilityCooldown = invincibilityCooldown;
        SetCurrentHealth(maxHealth);
    }

    /// <summary>
    /// Метод, устанавливающий текущее количество очков здоровья игрока
    /// </summary>
    public void SetCurrentHealth(int newCurrentHealth) => currentHealth = newCurrentHealth;
    
    /// <summary>
    /// Метод, уменьшающий количество очков здоровья игрока на переданное значение урона
    /// </summary>
    /// <param name="damage">Значение урона</param>
    public void TakeDamage(int damage)
    {
        if (Time.time - timeSinceLastDamageTaken > invincibilityCooldown)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            SetCurrentHealth(currentHealth);
            timeSinceLastDamageTaken = Time.time;
            OnDamageTaken?.Invoke(currentHealth, maxHealth, healthComponent);
            if(currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    /// <summary>
    /// Метод, уменьшающий количество очков здоровья игрока на переданное значение урона
    /// </summary>
    /// <param name="healPoints">Значение урона</param>
    /// <returns>True - если игрок здоровье было изменено, иначе - false</returns>
    public bool Heal(int healPoints)
    {
        if (currentHealth != maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + healPoints, maxHealth);
            SetCurrentHealth(currentHealth);
            OnHeal?.Invoke(currentHealth, maxHealth);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Проверка, обладает ли объект максимальным количеством очков здоровья
    /// </summary>
    public bool IsFullHealth()
    {
        return currentHealth == maxHealth;
    }

    /// <summary>
    /// Получить текущее количество очков здоровья
    /// </summary>
    /// <returns>Текущее количество очков здоровья</returns>
    public int GetCurrrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Получить максимальное количество очков здоровья
    /// </summary>
    /// <returns>Максимальное количество очков здоровья</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Уничтожение объекта
    /// </summary>
    public void Kill()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDamageTaken = null;
        OnHeal = null;
        OnDeath = null;
    }
}
