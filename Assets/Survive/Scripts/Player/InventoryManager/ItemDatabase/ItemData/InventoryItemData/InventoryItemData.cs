using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Items/Inventory Item Data")]
public class InventoryItemData : ItemData
{
    [Tooltip("Item image in the inventory")] 
    public Sprite InventoryImage;

    [Tooltip("Item description")] 
    public string Description;
}
