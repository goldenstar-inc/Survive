using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Items/Inventory Item Data")]
public class InventoryItemData : PickableItemData
{
    public Sprite InventoryImage;
    public AudioClip[] DropSound;
    public AudioClip ErrorSound;
    public string Description;
}
