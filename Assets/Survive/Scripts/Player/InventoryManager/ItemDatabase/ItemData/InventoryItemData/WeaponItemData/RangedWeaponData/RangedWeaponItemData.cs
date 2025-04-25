using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponItemData", menuName = "Items/Ranged Weapon Item Data")]
public class RangedWeaponItemData : WeaponItemData
{
    [Tooltip("Prefab пули")] 
    public GameObject BulletPrefab;

    [Tooltip("Скорость пули")] 
    [SerializeField, Range(10, 100)] public float BulletVelocity;

    [Tooltip("Время жизни пули")] 
    [SerializeField, Range(1, 10)] public float BulletLifeTime;
}
