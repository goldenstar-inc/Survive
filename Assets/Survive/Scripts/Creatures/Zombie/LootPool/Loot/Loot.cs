using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Loot/Loot")]
public class Loot : ScriptableObject
{
    public GameObject ItemPrefab;
    public float DropChance;
    public int MinAmount;
    public int MaxAmount;
}