using UnityEngine;

public interface IInventoryController
{
    public Item[] currentInventory { get; }
    public int inventoryCapacity { get; }
    public bool AddItemToInventory(IPickable pickable);
}
