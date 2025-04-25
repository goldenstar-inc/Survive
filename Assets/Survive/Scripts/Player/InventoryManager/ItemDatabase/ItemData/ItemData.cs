using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item Data")]
public class PickableItemData : ScriptableObject
{
    public PickableItems Name;
    
    [Tooltip("Prefab of the item to drop")] 
    public GameObject Prefab;
    public AudioClip[] PickUpSound;
}