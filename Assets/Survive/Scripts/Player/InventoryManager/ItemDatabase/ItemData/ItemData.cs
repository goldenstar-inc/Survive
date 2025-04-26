using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item Data")]
public class PickableItemData : IntreractableData
{
    public PickableItems Name;
    
    [Tooltip("Prefab of the item to drop")] 
    public GameObject Prefab;
}