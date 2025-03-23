using static InventoryController;
using UnityEngine;

/// <summary>
/// Интерфейс для оружий
/// </summary>
public interface IWeapon : IPickable
{
    /// <summary>
    /// Урон
    /// </summary>
    public int Damage { get; }

    /// <summary>
    /// Скорость атаки
    /// </summary>
    public int AttackSpeed { get; }

    /// <summary>
    /// Скрипт атаки для оружия
    /// </summary>
    public IAttackScript script { get; }
}