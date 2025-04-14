using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    private GameObject[] selectionFrames;
    private Sprite emptySlotImage;
    private Image[] inventoryItemImages;
    private TextMeshProUGUI[] itemQuanityTextFields;
    private InventoryController inventoryController;

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
        InventoryController inventoryController
    )
    {
        this.selectionFrames = selectionFrames;
        this.emptySlotImage = emptySlotImage;
        this.inventoryItemImages = inventoryItemImages;
        this.itemQuanityTextFields = itemQuanityTextFields;
        this.inventoryController = inventoryController;

        inventoryController.OnPickUp += AddItem;
        inventoryController.OnDrop += DecreaseItemAmount;
        inventoryController.OnUse += DecreaseItemAmount;
        inventoryController.OnChangeSelection += SelectSlot;

        for (int i = 0; i < inventoryItemImages.Length; i++)
        {
            inventoryItemImages[i].sprite = emptySlotImage;
        }
    }
    
    private void SelectSlot(int index)
    {
        if (!ValidateIndex(index)) return;

        foreach (GameObject selectionFrame in selectionFrames)
        {
            selectionFrame.SetActive(false);
        }

        selectionFrames[index].SetActive(true);
    }
    private void AddItem(int index, int quantity, InventoryItemData data)
    {
        if (!ValidateIndex(index)) return;
        if (!ValidateQuantity(quantity)) return;
        if (!ValidateData(data)) return;

        ShowItemQuantity(index, quantity);
        ShowItemImage(index, data);
    }
    private void DecreaseItemAmount(int index, int quantity)
    {
        if (!ValidateIndex(index)) return;
        if (!ValidateQuantity(quantity)) return;

        if (quantity == 0)
        {
            RemoveItem(index);
        }
        else
        {
            ShowItemQuantity(index, quantity);
        }
    }



    private void RemoveItem(int index)
    {
        inventoryItemImages[index].sprite = emptySlotImage;
        itemQuanityTextFields[index].text = string.Empty;
    }

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

    private void ShowItemImage(int index, InventoryItemData data)
    {
        inventoryItemImages[index].sprite = data.InventoryImage;
    }

    private bool ValidateIndex(int index)
    {
        if (index < 0 || index >= inventoryItemImages.Length)
        {
            Debug.LogError("Incorrect index");
            return false;
        }

        return true;
    }
    private bool ValidateQuantity(int quantity)
    {
        if (quantity < 0)
        {
            Debug.LogError("Incorrect quanity");
            return false;
        }

        return true;
    }
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