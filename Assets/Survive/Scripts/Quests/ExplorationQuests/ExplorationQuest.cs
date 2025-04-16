using System;
using UnityEngine;

/// <summary>
/// �����, �������������� �����
/// </summary>
[CreateAssetMenu(fileName = "ExplorationQuest", menuName = "Quests/Exploration Quest")]
public class ExplorationQuest : Quest
{
    public override event Action OnQuestCompleted;
    private QuestManager questManager;

    /// <summary>
    /// �������������
    /// </summary>
    /// <param name="questManager">������, ����������� ��������</param>
    public void Init(QuestManager questManager)
    {
        this.questManager = questManager;
    }
    
    /// <summary>
    /// ����� ���������� ������
    /// </summary>
    public void CompleteQuest()
    {
        OnQuestCompleted?.Invoke();
        questManager.CompleteQuest();
        Dispose();
    }

    /// <summary>
    /// ����������� �������
    /// </summary>
    public override void Dispose()
    {
    }
}
