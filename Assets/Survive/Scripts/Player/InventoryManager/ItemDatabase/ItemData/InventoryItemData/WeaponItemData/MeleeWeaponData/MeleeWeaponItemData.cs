using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponItemData", menuName = "Items/Melee Weapon Item Data")]
public class MeleeWeaponItemData : WeaponItemData
{
    [Tooltip("Attack range")] 
    [SerializeField] public float AttackRange;

    [Tooltip("Attack sound")] 
    public AudioClip[] SwingSounds;
}