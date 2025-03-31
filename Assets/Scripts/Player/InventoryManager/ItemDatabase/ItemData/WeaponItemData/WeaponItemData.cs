using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Items/Weapon Item Data")]
public class WeaponItemData : ItemData
{
    [Tooltip("Dealt damage")] 
    [SerializeField, Range(1, 200)] public int Damage;

    [Tooltip("Cooldown of an attack")] 
    [SerializeField, Range(0.1f, 10f)] public float AttackCooldown;
    [SerializeField] public AnimatorOverrideController Animator;
}
