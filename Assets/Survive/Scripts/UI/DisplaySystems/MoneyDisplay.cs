using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    private MoneyHandler moneyHandler;
    private TextMeshProUGUI balancePlaceholder;
    
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="moneyHandler">Класс, управляющий деньгами</param>
    public void Init(MoneyHandler moneyHandler, TextMeshProUGUI balancePlaceholder)
    {
        this.balancePlaceholder = balancePlaceholder;
        this.moneyHandler = moneyHandler;
        moneyHandler.OnBalanceChanged += UpdateBalance;
        UpdateBalance(moneyHandler.GetCurrentBalance());
    }
    public void UpdateBalance(int currentBalance)
    {
        balancePlaceholder.text = $"{currentBalance}";
    }
    void OnDisable()
    {
        if (moneyHandler != null)
        {
            moneyHandler.OnBalanceChanged -= UpdateBalance;
        }
    }
}
