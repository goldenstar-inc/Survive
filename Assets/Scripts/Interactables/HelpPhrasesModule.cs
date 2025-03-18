using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Модуль, содержащий словарь с возможными подсказками для игрока
/// </summary>
public static class HelpPhrasesModule
{
    public static Dictionary<Action, string> actionToPhrase = new Dictionary<Action, string>()
    {
        { Action.PickUp, "PICK UP [E]" },
        { Action.Open, "OPEN [F]" }
    };

    public enum Action
    {
        PickUp,
        Open
    }
}
