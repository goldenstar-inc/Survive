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
    /// <summary>
    /// Текстовое поле баланса игрока
    /// </summary>
    private TextMeshProUGUI currentBalancePlaceholder;

    /// <summary>
    /// Денежный баланс
    /// </summary>
    private int Balance; 

    /// <summary>
    /// Метод, который вызывается во время загрузки экземпляра сценария
    /// </summary>
    public void Init(TextMeshProUGUI currentBalancePlaceholder)
    {
        this.currentBalancePlaceholder = currentBalancePlaceholder;
        Balance = 0;
        UpdateUI();
    }

    /// <summary>
    /// Метод, вызывающийся для обновления текущего баланса в интерфейсе
    /// </summary>
    public void UpdateUI() 
    {
        //currentBalancePlaceholder.text = $"{Balance}";
    }

    /// <summary>
    /// Метод, вызывающийся для добавления денег к балансу
    /// </summary>
    public void AddMoney(int amount) 
    {
        Balance += amount;
        UpdateUI();
    }

    /// <summary>
    /// Метод, вызывающийся для оплаты 
    /// </summary>
    public void Pay(int amount)
    {
        Balance -= amount;
        Balance = Math.Max(0, Balance);
        UpdateUI();
    }

    /// <summary>
    /// Возвращает текущий денежный баланс
    /// </summary>
    /// <returns>Текущий денежный баланс</returns>
    public int GetCurrentBalance()
    {
        return Balance;
    }
}