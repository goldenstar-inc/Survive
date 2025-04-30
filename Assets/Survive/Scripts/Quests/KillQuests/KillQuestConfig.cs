using UnityEngine;

/// <summary>
///  ласс, представл€ющий квест, св€занный с убийством
/// </summary>
[CreateAssetMenu(fileName = "KillQuestConfig", menuName = "Quests/Kill Quest Config")]
public class KillQuestConfig : QuestConfig
{
    [SerializeField] public CreatureType QuestTargetType;
}
