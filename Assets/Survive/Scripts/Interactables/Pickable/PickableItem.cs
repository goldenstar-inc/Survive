using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий подбираемый объект
/// </summary>
public class PickableItem : MonoBehaviour, IInteractable, IPickable
{
    /// <summary>
    /// Информация об объекте
    /// </summary>
    [SerializeField] ItemData data;
    public ItemData Data => data;
    public PickableItems Name => data.Name;
    public int Quantity => quantity;
    private int quantity;

    /// <summary>
    /// Инициализация объекта
    /// </summary>
    /// <param name="quantity">Количество данного объекта</param>
    public void Initialize(int quantity = 1)
    {
        SetQuantity(quantity);
    }
    
    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public virtual void Interact(IPlayerDataProvider interactor)
    {
        Destroy(gameObject);
    }

    public void SetQuantity(int quantity)
    {
        if (quantity < 1)
        {
            Debug.LogError("В качестве колиечства предмета передано не допустимое значение!");
            return;
        }
        this.quantity = quantity;
    }
}
