using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemDatabase", menuName = "Items/Inventory Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<InventoryItemData> Items;
}
