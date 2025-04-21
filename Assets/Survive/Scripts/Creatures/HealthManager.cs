using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public event Action OnHealthChanged;
    public event Action<int, int> OnTakeDamage;
    public event Action<int, int> OnHeal;
    public event Action OnDeath;

    private int currentHealth;
    private int maxHealth;
    private  SoundController soundController;
    private AudioClip damageSound;
    private float invincibleCooldown;
    private float timeSinceLastDamageTaken = 0f;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    /// <param name="damageSound">Звук получения урона</param>
    /// <param name="invincibleCooldown">Время иммунитета после получения урона</param>
    /// <param name="soundController">Скрип, управляющий звуком</param>
    public void Init(int maxHealth, AudioClip damageSound, float invincibleCooldown, SoundController soundController) 
    {
        this.maxHealth = maxHealth;
        this.damageSound = damageSound;
        this.invincibleCooldown = invincibleCooldown;
        this.soundController = soundController;
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
        if (Time.time - timeSinceLastDamageTaken > invincibleCooldown)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            SetCurrentHealth(currentHealth);
            soundController?.PlayAudioClip(damageSound);
            timeSinceLastDamageTaken = Time.time;
            OnTakeDamage?.Invoke(currentHealth, maxHealth);
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
        OnTakeDamage = null;
        OnHeal = null;
        OnDeath = null;
    }
}
