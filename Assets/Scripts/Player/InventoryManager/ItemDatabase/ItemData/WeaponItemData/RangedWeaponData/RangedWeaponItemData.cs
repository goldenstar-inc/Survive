using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponItemData", menuName = "Items/Ranged Weapon Item Data")]
public class RangedWeaponItemData : WeaponItemData
{
    [Tooltip("Prefab пули")] 
    public GameObject BulletPrefab;

    [Tooltip("Скорость пули")] 
    public float BulletVelocity;

    [Tooltip("Время жизни пули")] 
    public float BulletLifeTime;

    [Tooltip("Звук выстрела")] 
    public SoundType ShotSound;
}
