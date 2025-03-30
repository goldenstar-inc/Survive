using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemUseLogic", menuName = "Items/Item Use Logic")]
public abstract class ItemUseLogic : ScriptableObject
{
    public abstract void Use();
}
