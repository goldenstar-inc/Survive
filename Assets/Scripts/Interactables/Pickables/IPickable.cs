using static InventoryController;
using UnityEngine;

/// <summary>
/// Интерфейс, содержащий свойство - тип поднимаемого объекта
/// </summary>
public interface IPickable : IInteractable
{
    /// <summary>
    /// Изображение предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage { get; }

    /// <summary>
    /// Тип поднимаемого объекта
    /// </summary>
    public PickableItems type { get; }
}