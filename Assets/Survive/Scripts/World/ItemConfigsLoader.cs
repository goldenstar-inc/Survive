using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Скрипт, отвечающий за инициализацию словаря, который содержит названия предметов и их конфигурации
/// </summary>
public static class ItemConfigsLoader
{
    /// <summary>
    /// Словарь, который содержит названия предметов и их конфигурации
    /// </summary>
    public static Dictionary<PickableItems, InventoryItemData> nameToData;

    /// <summary>
    /// Метод инициализации словаря
    /// </summary>
    public static void Initialize(List<InventoryItemData> configs)
    {
        nameToData = new Dictionary<PickableItems, InventoryItemData>();

        foreach (InventoryItemData config in configs)
        {
            PickableItems itemName = config.Name;

            if (!nameToData.ContainsKey(itemName))
            {
                nameToData[itemName] = config;
            }
        }
    }
}
