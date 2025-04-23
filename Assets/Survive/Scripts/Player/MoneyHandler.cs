using System;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за денежный баланс игрока
/// </summary>
public class MoneyHandler : MonoBehaviour
{
    public event Action<int> OnBalanceChanged;
    private int currentBalance; 

    /// <summary>
    /// Метод, который вызывается во время загрузки экземпляра сценария
    /// </summary>
    public void Init()
    {
        currentBalance = 0;
    }

    /// <summary>
    /// Подбор денег
    /// </summary>
    /// <param name="amount">Количество</param>
    public void Collect(int amount) 
    {
        currentBalance += amount;
        OnBalanceChanged?.Invoke(currentBalance);
    }

    /// <summary>
    /// Трата определенного количества денег
    /// </summary>
    /// <param name="amount">Количество</param>
    public void Spend(int amount)
    {
        currentBalance -= amount;
        currentBalance = Math.Max(0, currentBalance);
        OnBalanceChanged?.Invoke(currentBalance);
    }

    /// <summary>
    /// Возвращает текущий денежный баланс
    /// </summary>
    /// <returns>Текущий денежный баланс</returns>
    public int GetCurrentBalance()
    {
        return currentBalance;
    }
}