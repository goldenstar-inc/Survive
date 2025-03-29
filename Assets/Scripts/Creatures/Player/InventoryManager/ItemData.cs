using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Items;
}

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
}

[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Items/Weapon Item Data")]
public class WeaponItemData: ItemData
{
    [Tooltip("Dealt damage")] 
    [SerializeField, Range(1, 200)] public int Damage;

    [Tooltip("Cooldown of an attack")] 
    [SerializeField, Range(0.1f, 10f)] public float AttackCooldown;
}

[CreateAssetMenu(fileName = "StackableItemData", menuName = "Items/Stackable Item Data")]    
public class StackableItemData: ItemData
{
    [Tooltip("Max amount of the item in an inventory slot")] 
    [SerializeField, Range(1, 32)] public int MaxQuantity;
}

[CreateAssetMenu(fileName = "UsableItemData", menuName = "Items/Usable Item Data")]    
public class UsableItemData: StackableItemData
{
    [Tooltip("Usage time in seconds")] 
    [SerializeField, Range(1, 10)] public int UseDuration;
}