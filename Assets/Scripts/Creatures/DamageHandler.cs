using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Интерфейс, содержащий метод, вызывающийся при изменении очков здоровья игрока
/// </summary>
public interface IDamageObserver 
{
    void OnDamageTaken(int currentHealth, int maxHealth);
}

/// <summary>
/// Класс, отслеживающий получение урона
/// </summary>
public class DamageHandler : MonoBehaviour
{
    public SoundType damagedCreatureSound;
    public GameOverScreen gameOverScreen;
    private HealthManager manager;
    private List<IDamageObserver> observers = new List<IDamageObserver>();

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        manager = GetComponent<HealthManager>();
    }

    /// <summary>
    /// Метод, уменьшающий количество очков здоровья игрока на переданное значение урона
    /// </summary>
    /// <param name="damage">Значение урона</param>
    public void TakeDamage(int damage)
    {
        if (manager != null)
        {
            int currentHealth = manager.GetCurrentHealth();
            currentHealth = Mathf.Max(0, currentHealth - damage);
            int maxHealth = manager.GetMaxHealth();
            manager.SetCurrentHealth(currentHealth);
            NotifyObservers(currentHealth, maxHealth);
            SoundController.Instance.PlaySound(damagedCreatureSound, SoundController.Instance.playerStateAudioSource);
            if(currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    /// <summary>
    /// Метод, добавляющий нового наблюдателя в список наблюдателей
    /// </summary>
    /// <param name="newObserver">Новый наблюдатель</param>
    public void AddObserver(IDamageObserver newObserver) 
    {
        if (newObserver != null && !observers.Contains(newObserver))
        {
            observers.Add(newObserver);
        }
    }

    /// <summary>
    /// Уведомляет подписчиков события об изменении очков здоровья игрока
    /// </summary>
    public void NotifyObservers(int currentHealth, int maxHealth)
    {
        foreach (IDamageObserver observer in observers)
        {
            observer.OnDamageTaken(currentHealth, maxHealth);
        }
    }

    /// <summary>
    /// Метод, уничтожающий обьект
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);

        if(gameOverScreen != null)
        {
            gameOverScreen.Setup();
        }
    }
}
