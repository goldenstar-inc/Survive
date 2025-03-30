using UnityEngine;

[CreateAssetMenu(fileName = "StackableItemData", menuName = "Items/Stackable Item Data")]    
public class StackableItemData: ItemData
{
    [Tooltip("Max amount of the item in an inventory slot")] 
    [SerializeField, Range(1, 32)] public int MaxQuantity;
}
