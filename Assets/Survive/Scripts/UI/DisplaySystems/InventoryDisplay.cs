using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отображающий инвентарь
/// </summary>
public class InventoryDisplay : MonoBehaviour
{
    private GameObject[] selectionFrames;
    private Sprite emptySlotImage;
    private Image[] inventoryItemImages;
    private TextMeshProUGUI[] itemQuanityTextFields;
    private Inventory inventory;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="selectionFrames">Массив объектов, содержащих рамки выделения предмета</param>
    /// <param name="emptySlotImage">Спрайт пустого слота</param>
    /// <param name="inventoryItemImages">Массив, содержащий изображения подобранных предметов инвентаря</param>
    /// <param name="itemQuanityTextFields">Массив, содержащий текстовые поля, отображающие количество предмета в инвентаре </param>
    public void Init(
        GameObject[] selectionFrames,
        Sprite emptySlotImage,
        Image[] inventoryItemImages,
        TextMeshProUGUI[] itemQuanityTextFields,
        Inventory inventory
    )
    {
        this.selectionFrames = selectionFrames;
        this.emptySlotImage = emptySlotImage;
        this.inventoryItemImages = inventoryItemImages;
        this.itemQuanityTextFields = itemQuanityTextFields;
        this.inventory = inventory;

        inventory.OnItemPickedUp += AddItem;
        inventory.OnItemDropped += DecreaseItemAmount;
        inventory.OnItemUsed += DecreaseItemAmount;
        inventory.OnSelectionChanged += SelectSlot;

        for (int i = 0; i < inventoryItemImages.Length; i++)
        {
            inventoryItemImages[i].sprite = emptySlotImage;
        }
    }
    
    /// <summary>
    /// Смена активного слота
    /// </summary>
    /// <param name="index">Индекс нового активного слота</param>
    /// <param name="UISoundPack">Пак со звуками UI</param>
    private void SelectSlot(int index, UISoundPack UISoundPack)
    {
        if (!ValidateIndex(index)) return;

        foreach (GameObject selectionFrame in selectionFrames)
        {
            selectionFrame.SetActive(false);
        }

        selectionFrames[index].SetActive(true);
    }

    /// <summary>
    /// Отображение подобранного предмета
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <param name="quantity">Количество</param>
    /// <param name="data">Данные о подобранном предмете</param>
    private void AddItem(int index, int slotQuantity, int _, InventoryItemData data)
    {
        if (!ValidateIndex(index)) return;
        if (!ValidateQuantity(slotQuantity)) return;
        if (!ValidateData(data)) return;

        ShowItemQuantity(index, slotQuantity);
        ShowItemImage(index, data);
    }

    /// <summary>
    /// Выброс предмета
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <param name="quantity">Новое количество предмета</param>
    private void DecreaseItemAmount(int index, int quantity, InventoryItemData itemData)
    {
        if (!ValidateIndex(index)) return;
        if (!ValidateQuantity(quantity)) return;

        if (quantity <= 0)
        {
            RemoveItem(index);
        }
        else
        {
            ShowItemQuantity(index, quantity);
        }
    }

    /// <summary>
    /// Удаление предмета
    /// </summary>
    /// <param name="index"Индекс></param>
    private void RemoveItem(int index)
    {
        inventoryItemImages[index].sprite = emptySlotImage;
        itemQuanityTextFields[index].text = string.Empty;
    }

    /// <summary>
    /// Отображение количества предмета в инвентаре
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <param name="quantity">Количество</param>
    private void ShowItemQuantity(int index, int quantity)
    {
        if (quantity != 1)
        {
            itemQuanityTextFields[index].text = $"{quantity}";
        }
        else
        {
            itemQuanityTextFields[index].text = string.Empty;
        }
    }

    /// <summary>
    /// Отображение картинки предмета
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <param name="data"Данные предмета></param>
    private void ShowItemImage(int index, InventoryItemData data)
    {
        inventoryItemImages[index].sprite = data.InventoryImage;
    }

    /// <summary>
    /// Валидация индекса
    /// </summary>
    /// <param name="index">Индекс</param>
    /// <returns>True - если индекс корректный, иначе - false</returns>
    private bool ValidateIndex(int index)
    {
        if (index < 0 || index >= inventoryItemImages.Length)
        {
            Debug.LogError("Incorrect index");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Валидация количества
    /// </summary>
    /// <param name="quantity">Количество</param>
    /// <returns>True - если количество предмета корректное, иначе - false</returns>
    private bool ValidateQuantity(int quantity)
    {
        if (quantity < 0)
        {
            Debug.LogError("Incorrect quanity");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Валидация данных
    /// </summary>
    /// <param name="data">Данные</param>
    /// <returns>True - если данные переданы корректно, иначе - false</returns>
    private bool ValidateData(InventoryItemData data)
    {
        if (data == null)
        {
            Debug.LogError("Incorrect data");
            return false;
        }

        return true;
    }
}