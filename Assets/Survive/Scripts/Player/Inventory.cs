using UnityEngine;
using System;
using System.Collections.Generic;
using static ItemConfigsLoader;
using NUnit.Framework.Internal;

/// <summary>
/// Класс, представляющий инвентарь игрока
/// </summary>
public class Inventory : MonoBehaviour
{
    public event Action<int, int, int, InventoryItemData> OnItemPickedUp;
    public event Action<int, int, InventoryItemData> OnItemDropped;
    public event Action<int, int, InventoryItemData> OnItemUsed;
    public event Action<int, UISoundPack> OnSelectionChanged;
    public Item[] currentInventory { get; private set; }
    public int inventoryCapacity { get; private set; }
    private int currentPickableItemIndex;
    private UISoundPack UISoundPack;
    private Dictionary<PickableItems, int> itemToCount;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init(
        int inventoryCapacity,
        UISoundPack UISoundPack
        )
    {
        this.UISoundPack = UISoundPack;
        currentPickableItemIndex = 0;
        itemToCount = new ();
        this.inventoryCapacity = inventoryCapacity;
        currentInventory = new Item[inventoryCapacity];
    }

    /// <summary>
    /// Метод, меняющий текущий предмет в инвентаре
    /// </summary>
    public void ChangeSelectedItem(int change)
    {
        currentPickableItemIndex = (currentPickableItemIndex + change + inventoryCapacity) % inventoryCapacity;
        OnSelectionChanged?.Invoke(currentPickableItemIndex, UISoundPack);
    }

    /// <summary>
    /// Метод, пытающийся добавить предмет в инвентарь
    /// </summary>
    /// <param name="pickable">Предмет</param>
    /// <param name="data">Конфиг предмета</param>
    /// <param name="slotIndex">Индекс слота</param>
    /// <param name="slotQuantity">Количество предметов в слоте</param>
    /// <param name="entireQuantity">Количество предметов во всем инвентаре</param>
    /// <param name="extraAmount">Количество предмета которое надо выбросить</param>
    /// <returns>true - если получилось добавить, иначе - false</returns>
    public bool TryAdd(IPickable pickable, out int extraAmount, out InventoryItemData data)
    {
        extraAmount = 0;
        data = null;

        if (pickable != null)
        {
            data = GetConfigurationOfInventoryItem(pickable.Name);

            if (data != null)
            {
                Item newItem = new Item(data, pickable.Quantity);
                if (newItem.Data is StackableItemData stackableItemData)
                {
                    int freeSlotIndex = FindExistingItemIndex(stackableItemData);
                    
                    if (freeSlotIndex != -1)
                    {
                        int currentPickableQuantity = pickable.Quantity;
                        
                        if (!itemToCount.ContainsKey(pickable.Name))
                        {
                            itemToCount[pickable.Name] = currentPickableQuantity;
                        }
                        else
                        {
                            itemToCount[pickable.Name] += currentPickableQuantity;
                        }

                        if (currentInventory[freeSlotIndex] != null)
                        {
                            currentPickableQuantity += currentInventory[freeSlotIndex].Quantity;
                        } 
                        int maxPickableQuantity = stackableItemData.MaxQuantity;
                        
                        extraAmount = currentPickableQuantity - maxPickableQuantity;
                        
                        currentInventory[freeSlotIndex] = newItem;

                        if (extraAmount > 0)
                        {
                            newItem.Quantity = maxPickableQuantity;
                            itemToCount[pickable.Name] -= extraAmount;
                        }
                        else
                        {
                            newItem.Quantity = currentPickableQuantity;
                        }
                        OnItemPickedUp?.Invoke(freeSlotIndex, newItem.Quantity, itemToCount[pickable.Name], data);
                        return true;
                    }
                }
                else
                {
                    int freeSlotIndex = FindFreeSlotIndex();
                    if (freeSlotIndex != -1)
                    {
                        currentInventory[freeSlotIndex] = newItem;
                        if (!itemToCount.ContainsKey(pickable.Name))
                        {
                            itemToCount[pickable.Name] = newItem.Quantity;
                        }
                        else
                        {
                            itemToCount[pickable.Name] += 1;
                        }

                        OnItemPickedUp?.Invoke(freeSlotIndex, newItem.Quantity, itemToCount[pickable.Name], data);                    
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void Drop()
    {
        RemoveItem(false);
    }
    public void Use()
    {
        RemoveItem(true);
    }

    /// <summary>
    /// Метод удаления предмета из инвентаря
    /// </summary>
    /// <param name="slotIndex">Индекс слота</param>
    /// <param name="quantity">Количество</param>
    /// <param name="data">Конфиг предмета</param>
    public void RemoveItem(bool isUsed)
    {
        if (TryGetCurrentItem(out int index, out Item currentItem))
        {
            int quantity = currentItem.Quantity - 1;
            InventoryItemData data = currentItem.Data;
            if (quantity > 0)
            {
                currentItem.Quantity = quantity;
                itemToCount[data.Name] = quantity;
            }
            else
            {
                currentInventory[index] = null;
                itemToCount.Remove(data.Name);
            }

            if (isUsed)
            {
                OnItemUsed?.Invoke(index, quantity, data);
            }
            else
            {
                OnItemDropped?.Invoke(index, quantity, data);
            }
        }
    }

    /// <summary>
    /// Метод, возвращающий текущий выбранный предмет в инвенторе
    /// </summary>
    /// <returns>Текущий выбранный предмет в инвенторе</returns>
    public bool TryGetCurrentItem(out int index, out Item item)
    {
        if (currentInventory != null)
        {
            index = currentPickableItemIndex;
            item = currentInventory[currentPickableItemIndex];
            return true;
        }
        else
        {
            index = -1;
            item = null;
            return false;
        }
    }

    /// <summary>
    /// Метод получающий конфиг предмета
    /// </summary>
    /// <param name="itemName">Тип предмета</param>
    /// <returns>Конфиг предмета</returns>
    private InventoryItemData GetConfigurationOfInventoryItem(PickableItems itemName)
    {
        if (nameToData == null)
        {
            Debug.LogWarning("NameToData not loaded!");
            return null;
        }

        if (nameToData.ContainsKey(itemName))
        {
            InventoryItemData itemData = nameToData[itemName];
            return itemData;
        }
        else
        {
            Debug.LogError("Item not found");
            return null;
        }
    }
    /// <summary>
    /// Метод, получающий количество предметов по его типу
    /// </summary>
    /// <param name="name">Тип предмета</param>
    /// <returns>Количество предмета</returns>
    public int GetEntireItemCount(PickableItems name) => itemToCount.TryGetValue(name, out int count) ? count : 0;

    /// <summary>
    /// Метод, находящий индекс свободного слота в инвентаре
    /// </summary>
    /// <returns>Индекс свободного слота в инвентаре</returns>
    private int FindFreeSlotIndex()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (currentInventory[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Находит индекс существующего предмета в инвентаре
    /// </summary>
    /// <param name="newItemData">Конфиг предмета для добавления</param>
    /// <returns>Индекс существующего предмета в инвентаре</returns>
    private int FindExistingItemIndex(StackableItemData newItemData)
    {
        if (newItemData != null)
        {
            for (int i = 0; i < inventoryCapacity; i++)
            {
                if (currentInventory[i] != null && currentInventory[i].Data.Name == newItemData.Name)
                {
                    if (currentInventory[i].Quantity < newItemData.MaxQuantity)
                    {
                        return i;
                    }
                }
            }
        }

        return FindFreeSlotIndex();
    }

    /// <summary>
    /// Получение словаря содержащего количество предметов в инвентаре
    /// </summary>
    /// <returns></returns>
    public Dictionary<PickableItems, int> GetItemToCountMap()
    {
        return itemToCount;
    }

    public void RemoveItemsByRecipe(Recipe recipe)
    {
        List<RequiredItems> items = recipe.RequiredItems;

        if (items == null) return;

        foreach (RequiredItems item in items)
        {
            RemoveItemByName(item.Item, item.Amount);
        }
    }

    public void RemoveItemByName(PickableItems name, int amount)
    {
        int removedAmount = 0;

        for (int i = 0; i < inventoryCapacity && removedAmount < amount; i++)
        {
            if (currentInventory[i] != null && currentInventory[i].Data.Name == name)
            {
                int available = currentInventory[i].Quantity;
                int toRemove = Mathf.Min(available, amount - removedAmount);

                RemoveItemAtIndex(i, toRemove);
                removedAmount += toRemove;
            }
        }
    }

    private void RemoveItemAtIndex(int index, int quantity)
    {
        if (index < 0 || index >= inventoryCapacity || currentInventory[index] == null) return;

        var item = currentInventory[index];
        var data = item.Data;

        if (item.Quantity > quantity)
        {
            item.Quantity -= quantity;
            itemToCount[data.Name] -= quantity;
            OnItemDropped?.Invoke(index, item.Quantity, data);
        }
        else
        {
            quantity = item.Quantity;
            currentInventory[index] = null;
            itemToCount[data.Name] -= quantity;
            if (itemToCount[data.Name] <= 0)
            {
                itemToCount.Remove(data.Name);
            }
            OnItemDropped?.Invoke(index, 0, data);
        }
    }
}
