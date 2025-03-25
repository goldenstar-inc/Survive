using UnityEngine;

/// <summary>
/// Интерфейс, содержащий количество данного предмета
/// </summary>
public interface IAmountable
{
    /// <summary>
    /// Свойство, хранящее количество данного предмета
    /// </summary>
    public int Quanity { get; set; }
}
