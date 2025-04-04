using UnityEngine;

/// <summary>
/// Интерфейс, содержащий количество данного предмета
/// </summary>
public interface IAmountable
{
    /// <summary>
    /// Свойство, хранящее количество данного предмета
    /// </summary>
    public int Quantity { get; }

    /// <summary>
    /// Метод для задания количество данного предмета
    /// </summary>
    void SetQuantity(int quantity);
}
