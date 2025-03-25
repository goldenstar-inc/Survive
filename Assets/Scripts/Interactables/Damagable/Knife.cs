using UnityEditor;
using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;

/// <summary>
/// Класс, представляющий объект: "нож"
/// </summary>
public class Knife : MonoBehaviour, IInteractable, IPickable, IWeapon
{
    /// <summary>
    /// Спрайт ножа в инвентаре
    /// </summary>
    public Sprite knifeInventoryImage;

    /// <summary>
    /// Свойство, хранящее подсказку для игрока
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.PickUp];

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public PickableItems type => PickableItems.Knife;

    /// <summary>
    /// Свойство, хранящее спрайт предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage => knifeInventoryImage;

    /// <summary>
    /// Урон ножа
    /// </summary>
    public static int damage = 10;

    /// <summary>
    /// Свойство, хранящее урон оружия
    /// </summary>
    public int Damage => damage;

    /// <summary>
    /// Скорость выстрелов
    /// </summary>
    public static float attackSpeed =  0.1f;

    /// <summary>
    /// Свойство, хранящее скорость атаки оружия
    /// </summary>
    public float AttackSpeed => attackSpeed;

    /// <summary>
    /// Свойство, передающее метод атаки
    /// </summary>
    public IAttackScript script => FindAnyObjectByType<KnifeAttack>();

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }
}
