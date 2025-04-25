using static InventoryController;
using UnityEngine;

/// <summary>
/// Интерфейс, содержащий свойство - тип поднимаемого объекта
/// </summary>
public interface IPickable : IInteractable, IAmountable
{
    /// <summary>
    /// Информация об объекте
    /// </summary>
    public PickableItemData Data { get; }

    /// <summary>
    /// Тип поднимаемого объекта
    /// </summary>
    public PickableItems Name { get; }

    /// <summary>
    /// Звук подбора данного объекта
    /// </summary>
    public AudioClip[] PickUpSound { get; }
}