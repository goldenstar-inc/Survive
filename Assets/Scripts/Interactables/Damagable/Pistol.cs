using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;

/// <summary>
/// Класс, представляющий объект: "пистолет"
/// </summary>
public class Pistol : MonoBehaviour, IInteractable, IPickable, IWeapon
{
    /// <summary>
    /// Спрайт пистолета в инвентаре
    /// </summary>
    public Sprite pistolInventoryImage;

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public string helpPhrase { get => actionToPhrase[Action.PickUp]; }

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public PickableItems type { get => PickableItems.Pistol; }

    /// <summary>
    /// Свойство, хранящее спрайт предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage => pistolInventoryImage;

    /// <summary>
    /// Урон ножа
    /// </summary>
    public float damage = 10;

    /// <summary>
    /// Свойство, хранящее урон оружия
    /// </summary>
    public float Damage => damage;

    /// <summary>
    /// Скорость атаки ножа
    /// </summary>
    public float attackSpeed = 2;

    /// <summary>
    /// Свойство, хранящее скорость атаки оружия
    /// </summary>
    public float AttackSpeed => attackSpeed;

    /// <summary>
    /// Метод атаки
    /// </summary>
    public void Attack()
    {
        
    }

    /// <summary>
    /// �����, �������������� �������������� � ��������
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }
}