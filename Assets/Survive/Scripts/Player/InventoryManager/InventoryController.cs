using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static ItemConfigsLoader;
using static UseScriptFabric;
/// <summary>
/// �����, ���������� �� ������ ���������
/// </summary>
public class InventoryController : MonoBehaviour, IInventoryController
{
    [SerializeField] PlayerDataProvider playerData;
    
    /// <summary>
    /// Массив предметов, представляющих инвентарь игрока
    /// </summary>
    public Item[] currentInventory { get; private set; }

    /// <summary>
    /// Вместимость инвентаря
    /// </summary>
    public int inventoryCapacity { get; private set; }

    /// <summary>
    /// Индекс выбранного предмета в инвентаре
    /// </summary>
    private int currentPickableItemIndex;

    /// <summary>
    /// Массив объектов, содержащих рамки выделения предмета
    /// </summary>
    public GameObject[] selectedItemImages;

    /// <summary>
    /// Массив, содержащий изображения подобранных предметов инвентаря
    /// </summary>
    public Image[] inventoryItemImages;

    /// <summary>
    /// Спрайт пустого слота
    /// </summary>
    public Sprite emptySlotImage;
    
    /// <summary>
    /// Массив, содержащий текстовые поля, отображающие количество предмета в инвентаре 
    /// </summary>
    public TextMeshProUGUI[] itemQuanityTextFields;

    private IUseScript currentItemUseLogic;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip dropSound;
    
    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        inventoryCapacity = 2;
        currentPickableItemIndex = 0;
        currentInventory = new Item[inventoryCapacity];
        for (int i = 0; i < inventoryCapacity; i++)
        {
            currentInventory[i] = null;
            inventoryItemImages[i].sprite = emptySlotImage;
        }

        if (selectedItemImages == null)
        {
            Debug.LogWarning("SelectedItemImages not loaded!");
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
        else if (Input.GetKeyDown(KeyCode.Mouse4))
        {
            Drop();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Use();
        }
    }
    
    /// <summary>
    /// Метод, выделяющий предыдущий предмет в инвентаре
    /// </summary>
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

        Item selectedItem = GetCurrentPickableItem();

        if (selectedItem != null)
        {
            currentItemUseLogic = GetUseScript(selectedItem.Data, playerData);
        }
        
        playerData.SoundController?.PlayAudioClip(selectSound);
    }

    /// <summary>
    /// Метод, выделяющий следующий предмет в инвентаре
    /// </summary>
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

        Item selectedItem = GetCurrentPickableItem();
        
        if (selectedItem != null)
        {
            currentItemUseLogic = GetUseScript(selectedItem.Data, playerData);
        }
        
        playerData.SoundController?.PlayAudioClip(selectSound);
    }

    /// <summary>
    /// Метод, возвращающий текущий выбранный предмет в инвенторе
    /// </summary>
    /// <returns>Текущий выбранный предмет в инвенторе</returns>
    public Item GetCurrentPickableItem()
    {
        if (currentInventory != null)
        {
            return currentInventory[currentPickableItemIndex];
        }
        return null;
    }

    /// <summary>
    /// Функция использования предмета
    /// </summary>
    private void Use()
    {
        if (GetCurrentPickableItem() != null && GetCurrentPickableItem().Data is PickableItemData)
        {
            if (currentItemUseLogic != null)
            {
                bool wasUsed = currentItemUseLogic.Use();
                if (wasUsed)
                {
                    if (GetCurrentPickableItem().Data is StackableItemData)
                    {
                        RemoveElementFromInventory();
                    }
                }
                else
                {
                    if (GetCurrentPickableItem().Data is InventoryItemData data)
                    {
                        if (playerData is ISoundProvider soundProvider)
                        {
                            soundProvider.SoundController.PlayAudioClip(data.ErrorSound);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Метод, выбрасывающий предмет из инвентаря
    /// </summary>
    private void Drop()
    {
        if (currentInventory == null)
        {
            Debug.LogWarning("CurrentInventory not loaded");
            return;
        }
        Item itemToDrop = GetCurrentPickableItem();
        if (itemToDrop != null)
        {
            GameObject droppedItem = currentInventory[currentPickableItemIndex].Data.Prefab;
            Instantiate(droppedItem, transform.position, Quaternion.identity);
            RemoveElementFromInventory();
            AudioClip dropSound = itemToDrop.Data?.DropSound;
            playerData.SoundController?.PlayAudioClip(dropSound);
        }
    }

    /// <summary>
    /// Метод, удаляющий предмет из инвентаря игрока
    /// </summary>
    public void RemoveElementFromInventory()
    {
        if (GetCurrentPickableItem() != null && GetCurrentPickableItem() is Item item)
        {
            if (item.Data is StackableItemData)
            {
                int quantity = item.Quantity - 1;

                if (quantity > 0)
                {
                    item.Quantity = quantity;
                    itemQuanityTextFields[currentPickableItemIndex].text = $"{quantity}";
                }
                else
                {
                    currentInventory[currentPickableItemIndex] = null;
                    inventoryItemImages[currentPickableItemIndex].sprite = emptySlotImage;
                    itemQuanityTextFields[currentPickableItemIndex].text = string.Empty;
                }
            }
            else
            {
                currentInventory[currentPickableItemIndex] = null;
                inventoryItemImages[currentPickableItemIndex].sprite = emptySlotImage;
            }
        }
    }

    /// <summary>
    /// Метод, добавляющий подобранный предмет в инвентарь игрока
    /// </summary>
    /// <param name="pickable">Подобранный предмет</param>
    /// <returns>True- если предмет был добавлен в инвентар, иначе - false</returns>
    public bool AddItemToInventory(IPickable pickable)
    {
        if (pickable != null)
        {
            InventoryItemData newItemData = GetConfigurationOfInventoryItem(pickable.Name);
            
            if (newItemData != null)
            {
                Item newItem = new Item(newItemData, pickable.Quantity);

                if (newItem.Data is StackableItemData stackableItemData)
                {
                    int freeSlotIndex = FindExistingItemIndex(stackableItemData);
                    
                    if (freeSlotIndex != -1)
                    {
                        int currentPickableQuantity = pickable.Quantity;
                        if (currentInventory[freeSlotIndex] != null)
                        {
                            currentPickableQuantity += currentInventory[freeSlotIndex].Quantity;
                        } 
                        int maxPickableQuantity = stackableItemData.MaxQuantity;
                        
                        int extraAmount = currentPickableQuantity - maxPickableQuantity;
                        
                        currentInventory[freeSlotIndex] = newItem;
                        if (extraAmount > 0)
                        {
                            newItem.Quantity = maxPickableQuantity;
                            SpawnExtraItems(newItemData.Prefab, extraAmount);
                        }
                        else
                        {
                            newItem.Quantity = currentPickableQuantity;
                        }
                        SetInventoryImage(newItemData, freeSlotIndex);
                        ShowItemQuantity(freeSlotIndex, newItem.Quantity);
                        
                        if (newItem != null && currentPickableItemIndex == freeSlotIndex)
                        {
                            currentItemUseLogic = GetUseScript(newItem.Data, playerData);
                        }
                        return true;
                    }
                }
                else
                {
                    int freeSlotIndex = FindFreeSlotIndex();
                    if (freeSlotIndex != -1)
                    {
                        currentInventory[freeSlotIndex] = newItem;
                        SetInventoryImage(newItemData, freeSlotIndex);
                        if (newItem != null && currentPickableItemIndex == freeSlotIndex)
                        {
                            currentItemUseLogic = GetUseScript(newItem.Data, playerData);
                        }
                        return true;
                    }
                }
            }
        }

        return false;
    }

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
    /// Спавн объектов, которые не поместились в инвентарь
    /// </summary>
    /// <param name="itemPrefab">Префаб предмета</param>
    /// <param name="amount">Количество предмета</param>
    public void SpawnExtraItems(GameObject itemPrefab, int amount)
    {
        PickableItem pickableItemScript = itemPrefab.GetComponent<PickableItem>();
        pickableItemScript.Initialize(amount);
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
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

        return null;
    }

    /// <summary>
    /// Метод, отображающий изображение подобранного предмета в инвентаре игрока
    /// </summary>
    /// <param name="pickable">Подобранный предмет</param>
    /// <param name="freeItemIndex">Индекс свободного слота в инвентаре</param>
    private void SetInventoryImage(InventoryItemData data, int freeItemIndex)
    {
        if (inventoryItemImages != null && inventoryItemImages[freeItemIndex] != null)
        {
            inventoryItemImages[freeItemIndex].sprite = data.InventoryImage;
        }
    }

    /// <summary>
    /// Метод, отображающий количество предмета в инвентаре
    /// </summary>
    /// <param name="itemIndex">Индекс предмета</param>
    /// <param name="quanity">Количество</param>

    private void ShowItemQuantity(int itemIndex, int quanity)
    {
        if (itemQuanityTextFields == null)
        {
            Debug.LogWarning("ItemQuanityTextFields not loaded");
            return;
        }

        itemQuanityTextFields[itemIndex].text = $"{quanity}";
    }

}
