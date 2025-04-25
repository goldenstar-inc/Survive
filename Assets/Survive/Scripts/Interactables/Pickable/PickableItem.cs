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
    [SerializeField] PickableItemData data;
    public PickableItemData Data => data;
    public PickableItems Name => data.Name;
    public AudioClip[] PickUpSound => data.PickUpSound;
    public int Quantity => quantity;
    private int quantity;

    /// <summary>
    /// Инициализация объекта
    /// </summary>
    public void Start()
    {
        SetQuantity(1);
    }
    
    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    /// <param name="interactor">Взаимодействующий персонаж</param>
    /// <returns>True - если взаимодействие произошло успешно, иначе false</returns>
    public virtual bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
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
