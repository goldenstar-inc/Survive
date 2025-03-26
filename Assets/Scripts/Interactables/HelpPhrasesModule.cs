using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Модуль, содержащий подсказки для игрока
/// </summary>
public static class HelpPhrasesModule
{
    /// <summary>
    /// Словарь, хранящий пары ключ-значение, где ключ - "Действие", значение - "Сообщение для подсказки"
    /// </summary>
    public static Dictionary<Action, string> actionToPhrase = new Dictionary<Action, string>()
    {
        { Action.PickUp, "PICK UP [F]" },
        { Action.Open, "OPEN [T]" },
        { Action.InventoryFull, "INVENTORY FULL" },
        { Action.Buy, "BUY RANDOM STUFF [F]" }
    };

    /// <summary>
    /// Enum, содержащий возможные ситуации для подсказок
    /// </summary>
    public enum Action
    {
        PickUp,
        Open,
        InventoryFull,
        Buy,
    }
}
