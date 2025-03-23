using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;

/// <summary>
/// Класс, представляющий объект: "аптечка"
/// </summary>
public class Medkit : MonoBehaviour, IInteractable, IPickable
{
    /// <summary>
    /// Спрайт ножа в инвентаре
    /// </summary>
    public Sprite medkitInventoryImage;

    /// <summary>
    /// Свойство, хранящее подсказку для игрока
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.PickUp];

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public PickableItems type => PickableItems.Medkit;

    /// <summary>
    /// Свойство, хранящее спрайт предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage => medkitInventoryImage;

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>

    public void Interact()
    {
        Destroy(gameObject);
    }
}
