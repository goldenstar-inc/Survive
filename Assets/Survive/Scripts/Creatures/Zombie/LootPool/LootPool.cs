using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Loot/Lootpool")]
public class LootPool : ScriptableObject
{
    public List<Loot> Pool;
}