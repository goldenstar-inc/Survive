using static InventoryController;
using UnityEngine;

/// <summary>
/// Интерфейс, содержащий свойство - тип поднимаемого объекта
/// </summary>
public interface IWeapon : IPickable
{
    /// <summary>
    /// Урон
    /// </summary>
    public float Damage { get; }

    /// <summary>
    /// Скорость атаки
    /// </summary>
    public float AttackSpeed { get; }

    /// <summary>
    /// Метод атаки оружия
    /// </summary>
    public void Attack();
}