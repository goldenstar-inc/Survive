using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore;
using static ItemConfigsLoader;
using static UseScriptFabric;
/// <summary>
/// �����, ���������� �� ������ ���������
/// </summary>
public class InventoryController : MonoBehaviour
{    
    private PlayerDataProvider playerData;
    private IUseScript currentItemUseLogic;
    private PlayerPauseController playerPauseController;
    private bool isPaused = false;
    private UISoundPack UISoundPack;
    private Inventory inventory; 
    
    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init(
        PlayerDataProvider playerData,
        PlayerPauseController playerPauseController
        )
    {
        this.playerData = playerData;
        this.playerPauseController = playerPauseController;
        this.playerPauseController.OnPause += Pause;
        this.playerPauseController.OnResume += Unpause;

        if (playerData is IInventoryProvider inventoryProvider)
        {
            inventory = inventoryProvider.Inventory;
        }
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (isPaused) return;
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f)
        {
            ChangeSelection(1);
        }
        else if (scrollInput < 0f)
        {
            ChangeSelection(-1);
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
    private void ChangeSelection(int change)
    {
        inventory.ChangeSelectedItem(change);
    }

    /// <summary>
    /// Функция использования предмета
    /// </summary>
    private void Use()
    {
        if (inventory.TryGetCurrentItem(out int index, out Item currentItem))
        {
            if (currentItem != null && currentItem.Data is PickableItemData)
            {
                if (currentItemUseLogic != null)
                {
                    bool wasUsed = currentItemUseLogic.Use();
                    if (wasUsed)
                    {
                        if (currentItem.Data is StackableItemData data)
                        {
                            inventory.Use();
                        }
                    }
                }
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
        if (inventory.TryAdd(pickable, out int extraAmount, out InventoryItemData data))
        {
            SetCurrentUseScript();
            if (extraAmount > 0)
            {
                SpawnExtraItems(data.Prefab, extraAmount);
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Метод, выбрасывающий предмет из инвентаря
    /// </summary>
    private void Drop()
    {
       if (inventory.TryGetCurrentItem(out int index, out Item currentItem))
        {
            if (currentItem != null && currentItem.Data != null)
            {
                inventory.Drop();
                InventoryItemData data = currentItem.Data;
                GameObject droppedItem = data.Prefab;
                Instantiate(droppedItem, transform.position, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// Спавн объектов, которые не поместились в инвентарь
    /// </summary>
    /// <param name="itemPrefab">Префаб предмета</param>
    /// <param name="amount">Количество предмета</param>
    public void SpawnExtraItems(GameObject itemPrefab, int amount)
    {
        Debug.LogError("NOT DONE YET <3");
        PickableItem pickableItemScript = itemPrefab.GetComponent<PickableItem>();
        //pickableItemScript.Initialize(amount);
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Получение класса, отвечающего за использование объекта
    /// </summary>
    private void SetCurrentUseScript()
    {
        if (inventory.TryGetCurrentItem(out int currentItemIndex, out Item selectedItem))
        {
            if (selectedItem != null)
            {
                currentItemUseLogic = GetUseScript(selectedItem.Data, playerData);
            }
        }
    }

    private void Pause()
    {
        isPaused = true;
    }

    private void Unpause()
    {
        isPaused = false;
    }

    private void OnDisable()
    {
        playerPauseController.OnPause -= Pause;
        playerPauseController.OnResume -= Unpause;
    }
}