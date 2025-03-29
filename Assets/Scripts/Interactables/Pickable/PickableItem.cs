using UnityEngine;

/// <summary>
/// Класс, представляющий подбираемый объект
/// </summary>
public class PickableItem : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] ItemData data;
    public PickableItems Name => data.Name;
    private int quantity = 1;
    public int Quantity => quantity;

    /// <summary>
    /// Инициализация объекта
    /// </summary>
    /// <param name="quantity">Количество данного объекта</param>
    public void Initialize(int quantity)
    {
        this.quantity = quantity;
    }
    
    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }

    public void SetQuantity(int quantity)
    {
        throw new System.NotImplementedException();
    }
}
