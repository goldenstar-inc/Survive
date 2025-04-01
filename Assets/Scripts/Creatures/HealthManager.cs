using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : InitializableBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private SoundType damagedCreatureSound;
    public event Action<int, int> OnTakeDamage;
    public event Action<int, int> OnHeal;
    public event Action OnDeath;
    public int currentHealth { get; private set; }
    private float invincibleCooldown = 1.5f;
    private float timeSinceLastDamageTaken = 0f;
    
    /// <summary>
    /// Метод, инициализирующий здоровье игрока
    /// </summary>
    /// <param name="maxHealth">Максимальное кличество очков здоровья</param>
    public override void Initialize()
    {
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
            SoundController.Instance.PlaySound(damagedCreatureSound, SoundController.Instance.playerStateAudioSource);
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
    /// Уничтожение объекта
    /// </summary>
    public void Kill()
    {
        OnDeath?.Invoke();

        Destroy(gameObject);

        if (moneyPrefab != null)
        {
            DropMoney();
        }
    }

    /// <summary>
    /// Метод, отвечающий за выпадение денег
    /// </summary>
    private void DropMoney()
    {
        Instantiate(moneyPrefab, transform.position , Quaternion.identity);
    }
}
