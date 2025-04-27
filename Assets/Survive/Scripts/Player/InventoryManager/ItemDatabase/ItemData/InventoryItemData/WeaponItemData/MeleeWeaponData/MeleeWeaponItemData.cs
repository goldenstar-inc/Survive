using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponItemData", menuName = "Items/Melee Weapon Item Data")]
public class MeleeWeaponItemData : WeaponItemData
{
    [SerializeField] public GameObject AttackArea;
}