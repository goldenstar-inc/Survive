using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Интерфейс, содержащий метод, вызывающийся при изменении очков здоровья игрока
/// </summary>
public interface IHealObserver 
{
    void OnHealApplied(int currentHealth, int maxHealth);
}

/// <summary>
/// Класс, отслеживающий восполнение очков здоровья
/// </summary>
public class HealHandler : MonoBehaviour
{
    private HealthManager manager;
    private List<IHealObserver> observers = new List<IHealObserver>();

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
    /// <param name="healPoints">Значение урона</param>
    public void Heal(int healPoints)
    {
        if (manager != null)
        {
            int currentHealth = manager.GetCurrentHealth();
            int maxHealth = manager.GetMaxHealth();
            
            if (currentHealth != maxHealth)
            {
                currentHealth = Mathf.Min(currentHealth + healPoints, maxHealth);
                manager.SetCurrentHealth(currentHealth);
                NotifyObservers(currentHealth, maxHealth);
            }
        }
    }

    /// <summary>
    /// Метод, добавляющий нового наблюдателя в список наблюдателей
    /// </summary>
    /// <param name="newObserver">Новый наблюдатель</param>
    public void AddObserver(IHealObserver newObserver) 
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
        foreach (IHealObserver observer in observers)
        {
            observer.OnHealApplied(currentHealth, maxHealth);
        }
    }
}
