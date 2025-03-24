using TMPro;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за денежный баланс игрока
/// </summary>
public class MoneyHandler : MonoBehaviour
{
    /// <summary>
    /// Текущий экземпляр класса MoneyHandler
    /// </summary>
    public static MoneyHandler Instance { get; private set; }

    /// <summary>
    /// Текстовое поле баланса игрока
    /// </summary>
    public TextMeshProUGUI currentBalance;

    /// <summary>
    /// Денежный баланс
    /// </summary>
    public int Balance; 

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    private void Start() => Balance = 0;

    /// <summary>
    /// Метод, который вызывается во время загрузки экземпляра сценария
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UpdateUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Метод, вызывающийся для обновления текущего баланса в интерфейсе
    /// </summary>
    public void UpdateUI() => currentBalance.text = $"{Balance}";

    /// <summary>
    /// Метод, вызывающийся для добавления денег к балансу
    /// </summary>
    public void AddMoney(int amount) => Balance += amount;
}