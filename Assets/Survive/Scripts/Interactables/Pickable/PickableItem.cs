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
    public AudioClip PickSound => data.PickSound;
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
    /// <param name="interactor">Взаимодействующий персонаж</param>
    /// <returns>True - если взаимодействие произошло успешно, иначе false</returns>
    public virtual bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null)
        {
            Destroy(gameObject);
            if (interactor is ISoundProvider soundProvider)
            {
                soundProvider.SoundController?.PlayAudioClip(PickSound);
            }
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
