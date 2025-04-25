using System;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public event Action<CreatureType, HealthHandler> OnCreatureKilled;
    public event Action<int, PickableItems> OnItemPickedUp;
    public event Action<int, PickableItems> OnItemDropped;
    public event Action<int, PickableItems> OnItemUsed;
    private InventoryController inventoryController;

    public void Init(InventoryController inventoryController, WeaponManager weaponManager)
    {
        this.inventoryController = inventoryController;
        weaponManager.OnKill += CreatureKilled;
        inventoryController.OnItemPickedUp += ItemPickedUp;
        inventoryController.OnItemDropped += ItemDropped;
        inventoryController.OnItemUsed += ItemUsed;
    }
    private void CreatureKilled(CreatureType creatureType, HealthHandler killedCreature)
    {
        OnCreatureKilled?.Invoke(creatureType, killedCreature);
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
            inventoryController.OnItemPickedUp -= ItemPickedUp;
            inventoryController.OnItemDropped -= ItemDropped;
            inventoryController.OnItemUsed -= ItemUsed;
        }
        OnItemPickedUp = null;
        OnItemDropped = null;
        OnItemUsed = null;
    }
}
