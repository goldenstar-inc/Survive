using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    [Tooltip("Prefab of the item to drop")] 
    public GameObject Prefab;

    [Tooltip("Item image in the inventory")] 
    public Sprite InventoryImage;
    
    [Tooltip("Item name")] 
    public PickableItems Name;

    [Tooltip("Item description")] 
    public string Description;

    public virtual void Use()
    {
        Debug.Log("Bein' used");
    }
}
