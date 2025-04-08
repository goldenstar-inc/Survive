using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static InventoryController;

/// <summary>
/// Класс, представляющий предмет инвентаря
/// </summary>
public class Item
{
    /// <summary>
    /// Конфиг предмета
    /// </summary>
    public InventoryItemData Data { get; private set; } 
    private int quantity;

    /// <summary>
    /// Количество данного предмета
    /// </summary>
    public int Quantity 
    {
        get => quantity;
        set => quantity = value; 
    }

    /// <summary>
    /// Конструктор класса Item
    /// </summary>
    /// <param name="data">Конфигурационный файл</param>
    /// <param name="quantity">Колчество данного предмета</param>
    public Item(InventoryItemData data, int quantity = 1)
    {
        Data = data;
        this.quantity = quantity;
    }
}