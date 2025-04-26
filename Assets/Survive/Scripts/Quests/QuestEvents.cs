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
    private void ItemPickedUp(int _, int __, int entireQuantity, InventoryItemData data)
    {
        PickableItems itemType = data.Name;
        OnItemPickedUp?.Invoke(entireQuantity, itemType);
    }

    private void ItemDropped(int _, int currentQuantity, InventoryItemData data)
    {
        PickableItems itemType = data.Name;
        OnItemDropped?.Invoke(currentQuantity, itemType);
    }
    private void ItemUsed(int _, int currentQuantity, InventoryItemData data)
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
