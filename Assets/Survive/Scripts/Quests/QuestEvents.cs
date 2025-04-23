using System;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public event Action<int, PickableItems> OnItemPickedUp;
    public event Action<int> OnItemDropped;

    private InventoryController inventoryController;

    public void Init(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;
        inventoryController.OnPickUp += ItemPickedUp;
        inventoryController.OnDrop += ItemDropped;
    }
    private void ItemPickedUp(int slot, int quantity, InventoryItemData data)
    {
        quantity = 1;
        PickableItems itemType = data.Name;
        OnItemPickedUp?.Invoke(quantity, itemType);
    }

    private void ItemDropped(int slot, int quantity)
    {
        quantity = 1;
        OnItemDropped?.Invoke(quantity);
    }

    private void OnDestroy()
    {
        if (inventoryController != null)
        {
            inventoryController.OnPickUp -= ItemPickedUp;
            inventoryController.OnDrop -= ItemDropped;
        }
        OnItemPickedUp = null;
        OnItemDropped = null;
    }
}
