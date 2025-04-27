using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Items/Weapon Item Data")]
public class WeaponItemData : InventoryItemData
{
    [SerializeField, Range(1, 200)] public int Damage;
    [SerializeField, Range(0.1f, 10f)] public float AttackCooldown;
    [SerializeField] public AnimatorOverrideController Animator;
    [SerializeField] public AudioClip[] AttackSounds;
}
