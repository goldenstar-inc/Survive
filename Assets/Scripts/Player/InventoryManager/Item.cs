using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static InventoryController;

/// <summary>
/// Класс, представляющий предмет инвентаря
/// </summary>
public abstract class Item
{
    /// <summary>
    /// Уникальное имя
    /// </summary>
    public PickableItems UniqueName { get; set; }

    /// <summary>
    /// Конструктор класса Item
    /// </summary>
    /// <param name="name">Уникальное имя предмета</param>
    public Item(PickableItems name)
    {
        UniqueName = name;
    }
}

/// <summary>
/// Класс, представляющий оружие как предмет инвентаря
/// </summary>
public class DamagableItem : Item
{
    /// <summary>
    /// Урон
    /// </summary>
    public float Damage { get; set;}

    /// <summary> 
    /// Скорость атаки
    /// </summary>
    public float AttackSpeed { get; set; }

    /// <summary>
    /// Скрипт, хранящий функцию атаки
    /// </summary>
    public IAttackScript AttackScript;

    /// <summary>
    /// Конструктор класса DamagableItem
    /// </summary>
    /// <param name="name">Уникальное имя</param>
    /// <param name="damage">Урон</param>
    /// <param name="attackSpeed">Скорость атаки</param>
    public DamagableItem(PickableItems name, float damage, float attackSpeed, IAttackScript attackScript) : base (name)
    {
        Damage = damage;
        AttackSpeed = attackSpeed;
        AttackScript = attackScript;
    }
}

/// <summary>
/// Класс, представляющий стакающиеся предметы как предмет инвентаря
/// </summary>
public class StackableItem : Item
{
    /// <summary>
    /// Количество
    /// </summary>
    public int Quanity { get; set; }
    
    /// <summary>
    /// Скрипт, хранящий функцию использования предмета
    /// </summary>
    public IUseScript UseScript;

    /// <summary>
    /// Конструктор класса StackableItem
    /// </summary>
    /// <param name="name">Уникальное имя</param>
    /// <param name="quanity">Количество</param>
    public StackableItem(PickableItems name, int quanity, IUseScript useScript) : base(name)
    {
        Quanity = quanity;
        UseScript = useScript;
    }
}

