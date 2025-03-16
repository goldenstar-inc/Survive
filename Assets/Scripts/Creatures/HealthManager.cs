using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth { get; private set; }
    public int currentHealth { get; private set; }

    private DamageHandler damageHandler;
    private HealHandler healHandler;

    /// <summary>
    /// Метод, инициализирующий здоровье игрока
    /// </summary>
    /// <param name="maxHealth">Максимальное кличество очков здоровья</param>
    public void Initialize(int maxHealth, List<IDamageObserver> damageObservers, List<IHealObserver> healObservers)
    {
        SetMaxHealth(maxHealth);
        SetCurrentHealth(maxHealth);
        
        damageHandler = GetComponent<DamageHandler>();

        if (damageHandler != null) 
        {
            foreach (IDamageObserver damageObserver in damageObservers)
            {
                damageHandler.AddObserver(damageObserver);
            }
        }

        healHandler = GetComponent<HealHandler>();

        if (healHandler != null) 
        {
            foreach (IHealObserver healObserver in healObservers)
            {
                healHandler.AddObserver(healObserver);
            }
        }
    }

    /// <summary>
    /// Метод, возвращающий текущее количество очков здоровья игрока
    /// </summary>
    /// <returns>Текущее количество очков здоровья игрока</returns>
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Метод, возвращающий максимальное количество очков здоровья игрока
    /// </summary>
    /// <returns>Максимальное количество очков здоровья игрока</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Метод, устанавливающий текущее количество очков здоровья игрока
    /// </summary>
    public void SetCurrentHealth(int newCurrentHealth)
    {
        currentHealth = newCurrentHealth;
    }

    /// <summary>
    /// Метод, устанавливающий текущее количество очков здоровья игрока
    /// </summary>
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }
}
