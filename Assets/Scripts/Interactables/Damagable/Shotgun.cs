using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;

/// <summary>
/// Класс, представляющий объект: "дробовик"
/// </summary>
public class Shotgun : MonoBehaviour, IInteractable, IPickable, IWeapon
{
    /// <summary>
    /// Спрайт дробовика в инвентаре
    /// </summary>
    public Sprite shotgunInventoryImage;
    
    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public string helpPhrase { get => actionToPhrase[Action.PickUp]; }

    /// <summary>
    /// Свойство, содержащее тип поднимаемого объекта
    /// </summary>
    public PickableItems type { get => PickableItems.Shotgun; }

    /// <summary>
    /// Свойство, хранящее спрайт предмета в инвентаре
    /// </summary>
    public Sprite inventoryImage => shotgunInventoryImage;

    /// <summary>
    /// Урон ножа
    /// </summary>
    public int damage = 30;

    /// <summary>
    /// Свойство, хранящее урон оружия
    /// </summary>
    public int Damage => damage;

    /// <summary>
    /// Скорость атаки ножа
    /// </summary>
    public int attackSpeed = 1;

    /// <summary>
    /// Свойство, хранящее скорость атаки оружия
    /// </summary>
    public int AttackSpeed => attackSpeed;

    /// <summary>
    /// Скрипт, хранящий метод атаки
    /// </summary>
    public IAttackScript shotgunAttackScript;

    /// <summary>
    /// Свойство, передающее метод атаки
    /// </summary>
    public IAttackScript script => shotgunAttackScript;

    /// <summary>
    /// �����, �������������� �������������� � ��������
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }
}