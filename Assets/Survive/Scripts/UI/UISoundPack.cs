using UnityEngine;

[CreateAssetMenu(fileName = "UISoundPack", menuName = "Sounds/UISoundPack")]
public class UISoundPack : ScriptableObject
{
    [SerializeField] public AudioClip[] InventorySelectionChanged;
}
