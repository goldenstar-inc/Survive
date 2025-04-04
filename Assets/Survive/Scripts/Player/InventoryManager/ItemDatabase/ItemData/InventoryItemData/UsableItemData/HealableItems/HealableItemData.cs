using UnityEngine;

[CreateAssetMenu(fileName = "HealableItemData", menuName = "Items/Healable Item Data")]    
public class HealableItemData : UsableItemData
{
    [Tooltip("How many health points will be healed")] 
    [SerializeField, Range(1, 6)] public int HealPoints;
}
