using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Items/Inventory Item Data")]
public class InventoryItemData : PickableItemData
{
    [Tooltip("Item image in the inventory")] 
    public Sprite InventoryImage;

    [Tooltip("Dropped item sound")] 
    public AudioClip DropSound;

    [Tooltip("An error sound occured, while using the item")] 
    public AudioClip ErrorSound;

    [Tooltip("Item description")] 
    public string Description;
}
