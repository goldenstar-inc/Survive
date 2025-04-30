using UnityEngine;
using System.Collections.Generic;
using static ItemConfigsLoader;

/// <summary>
/// Класс, представляющий инвентарь игрока
/// </summary>
public class Inventory : MonoBehaviour
{
    public Item[] currentInventory { get; private set; }
    public int inventoryCapacity { get; private set; }
    private int currentPickableItemIndex;
    private Dictionary<PickableItems, int> itemToCount;

    public void Init(int inventoryCapacity)
    {
        currentPickableItemIndex = 0;
        itemToCount = new ();
        this.inventoryCapacity = inventoryCapacity;
        currentInventory = new Item[inventoryCapacity];
    }

    /// <summary>
    /// Метод, меняющий текущий предмет в инвентаре
    /// </summary>
    public void ChangeSelectedItem(int change, out int currentIndex)
    {
        currentPickableItemIndex = (currentPickableItemIndex + change + inventoryCapacity) % inventoryCapacity;
        currentIndex = currentPickableItemIndex;
    }

    public bool TryAdd(IPickable pickable, out InventoryItemData data, out int slotIndex, out int slotQuantity, out int entireQuantity, out int extraAmount)
    {
        slotIndex = -1;
        slotQuantity = 0;
        entireQuantity = 0;
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
                        slotIndex = freeSlotIndex;
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
                            slotQuantity = maxPickableQuantity;
                            itemToCount[pickable.Name] -= extraAmount;
                        }
                        else
                        {
                            slotQuantity = currentPickableQuantity;
                        }

                        newItem.Quantity = slotQuantity;
                        entireQuantity = itemToCount[pickable.Name];
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
                        slotIndex = freeSlotIndex;
                        slotQuantity = newItem.Quantity;
                        entireQuantity = itemToCount[pickable.Name];                        
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void RemoveItem(out int slotIndex, out int quantity, out InventoryItemData data)
    {
        slotIndex = -1;
        quantity = 0;
        data = null;

        if (TryGetCurrentItem(out int index, out Item currentItem))
        {
            slotIndex = index;
            quantity = currentItem.Quantity - 1;
            data = currentItem.Data;
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

    public Dictionary<PickableItems, int> GetItemToCountMap()
    {
        return itemToCount;
    }
}
