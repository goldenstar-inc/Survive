using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;

/// <summary>
/// Класс, представляющий объект: "кола"
/// </summary>
public class Coke : MonoBehaviour, IInteractable, IPickable, IUsable
{
    /// <summary>
    /// Спрайт колы в инвентаре
    /// </summary>
    public Sprite cokeInventoryImage;

    /// <summary>
    /// Свойство, хранящее подсказку для игрока
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.PickUp];

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public PickableItems type => PickableItems.Coke;

    /// <summary>
    /// Свойство, хранящее спрайт предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage => cokeInventoryImage;

    /// <summary>
    /// Скрипт, реализующий механику использования колы
    /// </summary>
    public IUseScript script => FindAnyObjectByType<CokeUse>();

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
