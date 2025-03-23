using System;
using JetBrains.Annotations;
using UnityEngine;
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

    /// <summary>
    /// Абстрактный метод использования предмета
    /// </summary>
    public abstract void Use();
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
    /// Делегат, хранящий функцию атаки
    /// </summary>
    public Action PerformAttack;

    /// <summary>
    /// Конструктор класса DamagableItem
    /// </summary>
    /// <param name="name">Уникальное имя</param>
    /// <param name="damage">Урон</param>
    /// <param name="attackSpeed">Скорость атаки</param>
    public DamagableItem(PickableItems name, float damage, float attackSpeed) : base (name)
    {
        Damage = damage;
        AttackSpeed = attackSpeed;
    }

    /// <summary>
    /// Переопределенный метод использования предмета
    /// </summary>
    public override void Use()
    {
        PerformAttack();
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
    /// Конструктор класса StackableItem
    /// </summary>
    /// <param name="name">Уникальное имя</param>
    /// <param name="quanity">Количество</param>
    public StackableItem(PickableItems name, int quanity) : base(name)
    {
        Quanity = quanity;
    }

    /// <summary>
    /// Переопределенный метод использования предмета
    /// </summary>
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}

