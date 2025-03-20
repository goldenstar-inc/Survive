using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за работу инвентаря
/// </summary>
public class InventoryController : MonoBehaviour
{
    /// <summary>
    /// Enum, содержащий все предметы, доступные игроку для подбора
    /// </summary>
    public enum PickableItems
    {
        None,
        Knife,
        Pistol,
        Shotgun,
        Medkit,
        Grenade
    }

    /// <summary>
    /// Массив предметов, представляющий инвентарь
    /// </summary>
    public PickableItems[] currentInventory { get; set; }

    /// <summary>
    /// Вместимость инвентаря
    /// </summary>
    private int inventoryCapacity;

    /// <summary>
    /// Индекс текущий предмет который держит игрок
    /// </summary>
    private int currentPickableItemIndex;

    /// <summary>
    /// Массив изображений выбранного предмета
    /// </summary>
    public GameObject[] selectedItemImages;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        inventoryCapacity = 2;
        currentPickableItemIndex = 0;
        currentInventory = new PickableItems[inventoryCapacity];
        for (int i = 0; i < inventoryCapacity; i++)
        {
            currentInventory[i] = PickableItems.None;
        }
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNextItem();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectPreviousItem();
        }
    }

    private void SelectNextItem()
    {
        if (selectedItemImages != null)
        {
            selectedItemImages[currentPickableItemIndex].SetActive(false);
        }

        currentPickableItemIndex = (currentPickableItemIndex + 1) % inventoryCapacity;

        if (selectedItemImages != null)
        {
            selectedItemImages[currentPickableItemIndex].SetActive(true);
        }
    }

    private void SelectPreviousItem()
    {
        if (selectedItemImages != null)
        {
            selectedItemImages[currentPickableItemIndex].SetActive(false);
        }

        currentPickableItemIndex = (currentPickableItemIndex - 1 + inventoryCapacity) % inventoryCapacity;
        
        if (selectedItemImages != null)
        {
            selectedItemImages[currentPickableItemIndex].SetActive(true);
        }
    }

    public PickableItems? GetCurrentPickableItem()
    {
        if (currentInventory != null)
        {
            return currentInventory[currentPickableItemIndex];
        }
        return null;
    }

    private void Use()
    {

    }

    private void Drop()
    {

    }

    public void AddItemToInventory(PickableItems newPickableItem)
    {
        if (currentInventory != null)
        {
            if (currentInventory[currentPickableItemIndex] == PickableItems.None)
            {
                currentInventory[currentPickableItemIndex] = newPickableItem;
            }
            else
            {
                int freeItemIndex = FindFreeSlot();
                if (freeItemIndex == -1)
                {
                    return;
                }
                currentInventory[freeItemIndex] = newPickableItem;
            }
        }
    }

    private int FindFreeSlot()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (currentInventory[i] == PickableItems.None)
            {
                return i;
            }
        }
        return -1;
    }
}
