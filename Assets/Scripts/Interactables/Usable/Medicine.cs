using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;

/// <summary>
/// Класс, представляющий объект: "витамины"
/// </summary>
public class Medicine : MonoBehaviour, IInteractable, IPickable, IUsable
{
    /// <summary>
    /// Спрайт витаминов в инвентаре
    /// </summary>
    public Sprite medicineInventoryImage;

    /// <summary>
    /// Свойство, хранящее подсказку для игрока
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.PickUp];

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public PickableItems type => PickableItems.Medicine;

    /// <summary>
    /// Свойство, хранящее спрайт предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage => medicineInventoryImage;

    /// <summary>
    /// Скрипт, реализующий механику использования витаминов
    /// </summary>
    public IUseScript script => FindAnyObjectByType<MedicineUse>();

    /// <summary>
    /// Свойство, возвращающее количество данного предмета
    /// </summary>
    public int Quanity { get; set; }

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    private void Start() => Quanity = 1;

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }
}
