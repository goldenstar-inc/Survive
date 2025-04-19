using UnityEngine;

[CreateAssetMenu(fileName = "KillQuestConfig", menuName = "Quests/Kill Quest Config")]
public class KillQuestConfig : QuestConfig
{
    [SerializeField] public CreatureType QuestTargetType { get; }
}
