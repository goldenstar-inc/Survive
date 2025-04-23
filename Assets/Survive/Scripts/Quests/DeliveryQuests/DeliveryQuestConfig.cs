using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий квест, связанный с доставкой предметов
/// </summary>
[CreateAssetMenu(fileName = "DeliveryQuestConfig", menuName = "Quests/Delivery Quest Config")]
public class DeliveryQuestConfig : QuestConfig
{
    [SerializeField] public PickableItems QuestItem;
}
