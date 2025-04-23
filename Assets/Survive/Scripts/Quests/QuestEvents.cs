using System;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public event Action<int, PickableItems> OnItemPickedUp;
    public event Action<int, PickableItems> OnItemDropped;
    public event Action<int, PickableItems> OnItemUsed;
    private InventoryController inventoryController;

    public void Init(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
        inventoryController.OnPickUp += ItemPickedUp;
        inventoryController.OnDrop += ItemDropped;
        inventoryController.OnUse += ItemUsed;
    }
    private void ItemPickedUp(int slot, int currentQuantity, InventoryItemData data)
    {
        PickableItems itemType = data.Name;
        OnItemPickedUp?.Invoke(currentQuantity, itemType);
    }

    private void ItemDropped(int slot, int currentQuantity, InventoryItemData data)
    {
        PickableItems itemType = data.Name;
        OnItemDropped?.Invoke(currentQuantity, itemType);
    }
    private void ItemUsed(int slot, int currentQuantity, InventoryItemData data)
    {
        PickableItems itemType = data.Name;
        OnItemUsed?.Invoke(currentQuantity, itemType);
    }

    private void OnDestroy()
    {
        if (inventoryController != null)
        {
            inventoryController.OnPickUp -= ItemPickedUp;
            inventoryController.OnDrop -= ItemDropped;
            inventoryController.OnUse -= ItemUsed;
        }
        OnItemPickedUp = null;
        OnItemDropped = null;
        OnItemUsed = null;
    }
}
