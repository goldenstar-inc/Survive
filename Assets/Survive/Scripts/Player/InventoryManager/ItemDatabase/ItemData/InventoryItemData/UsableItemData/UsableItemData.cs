using UnityEngine;

[CreateAssetMenu(fileName = "UsableItemData", menuName = "Items/Usable Item Data")]    
public class UsableItemData: StackableItemData
{
    [Tooltip("Usage time in seconds")] 
    [SerializeField, Range(1, 10)] public int UseDuration;
    
    [Tooltip("Usage sound")] 
    [SerializeField] public AudioClip[] Sound;
}