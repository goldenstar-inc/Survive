using UnityEngine;

/// <summary>
/// �����, �������������� �����, ��������� � ���������
/// </summary>
[CreateAssetMenu(fileName = "KillQuestConfig", menuName = "Quests/Kill Quest Config")]
public class KillQuestConfig : QuestConfig
{
    [SerializeField] public CreatureType QuestTargetType;
}
