using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item Data")]
public class PickableItemData : ScriptableObject
{
    [Tooltip("Item name")] 
    public PickableItems Name;

    [Tooltip("Prefab of the item to drop")] 
    public GameObject Prefab;

    [Tooltip("Sound played, when the item is picked up")] 
    public AudioClip PickSound;
}