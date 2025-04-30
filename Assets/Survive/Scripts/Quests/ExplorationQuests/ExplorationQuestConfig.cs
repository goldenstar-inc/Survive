using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий квест, связанный с исследованием зон
/// </summary>
[CreateAssetMenu(fileName = "ExplorationQuestConfig", menuName = "Quests/Exploration Quest Config")]
public class ExplorationQuestConfig : QuestConfig
{
    [SerializeField] public float X;
    [SerializeField] public float Y;
    [SerializeField] public float Radius;
}
