using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий подбираемый объект
/// </summary>
public class PickableItem : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] PickableItemData data;
    public event Action OnInteract;
    public IntreractableData Data => data;
    public PickableItems Name => data.Name;
    public AudioClip[] PickUpSound => data.InteractionSound;
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
            OnInteract?.Invoke();
            Destroy(transform.root.gameObject);
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
    protected void InvokeInteract()
    {
        OnInteract?.Invoke();
    }
}
