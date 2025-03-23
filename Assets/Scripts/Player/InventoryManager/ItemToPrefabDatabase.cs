using UnityEngine;
using static InventoryController;

/// <summary>
/// Класс, содержащий предметы и их префабы
/// </summary>

[CreateAssetMenu(fileName = "ItemToPrefabDatabase", menuName = "ItemToPrefab/ItemToPrefabDatabase")]
public class ItemToPrefabDatabase : ScriptableObject
{
    [System.Serializable]
    public class ItemToPrefabEntry
    {
        /// <summary>
        /// Уникальное название
        /// </summary>
        public PickableItems uniqueName;

        /// <summary>
        /// Префаб оружия
        /// </summary>
        public GameObject prefab;
    }

    /// <summary>
    /// Массив всех доступных оружий и их префабов
    /// </summary>
    public ItemToPrefabEntry[] ItemToPrefabPairs;
}
