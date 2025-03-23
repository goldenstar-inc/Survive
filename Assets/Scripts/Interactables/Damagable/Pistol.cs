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
    public int damage = 10;

    /// <summary>
    /// Свойство, хранящее урон оружия
    /// </summary>
    public int Damage => damage;

    /// <summary>
    /// Скорость атаки ножа
    /// </summary>
    public int attackSpeed = 2;

    /// <summary>
    /// Свойство, хранящее скорость атаки оружия
    /// </summary>
    public int AttackSpeed => attackSpeed;

    /// <summary>
    /// Скрипт, хранящий метод атаки
    /// </summary>
    public IAttackScript pistolAttackScript;

    /// <summary>
    /// Свойство, передающее метод атаки
    /// </summary>
    public IAttackScript script => pistolAttackScript;

    /// <summary>
    /// �����, �������������� �������������� � ��������
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }
}