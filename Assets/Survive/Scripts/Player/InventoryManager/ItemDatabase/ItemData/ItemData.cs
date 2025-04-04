using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [Tooltip("Item name")] 
    public PickableItems Name;

    [Tooltip("Prefab of the item to drop")] 
    public GameObject Prefab;
}