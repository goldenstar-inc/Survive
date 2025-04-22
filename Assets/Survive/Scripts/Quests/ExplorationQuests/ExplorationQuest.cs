using System;
using UnityEngine;

/// <summary>
/// �����, �������������� �����
/// </summary>
public class ExplorationQuest : IQuest
{
    private ExplorationQuestConfig questConfig;

    public event Action OnCompleted;
    private QuestManager questManager;

    private float x => questConfig.X;
    private float y => questConfig.Y;
    private float radius => questConfig.Radius;
    public QuestConfig QuestConfig => questConfig;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questConfig">Конфиг квеста</param>
    public ExplorationQuest(ExplorationQuestConfig questConfig)
    {
        this.questConfig = questConfig;
        CreateQuestZone();
    }

    public void SetQuestManager(QuestManager questManager)
    {
        this.questManager = questManager;    
    }

    /// <summary>
    /// Create quest zone
    /// </summary>
    private void CreateQuestZone()
    {
        GameObject questZone = new GameObject("QuestZone");
        CircleCollider2D questCollider = questZone.AddComponent<CircleCollider2D>();
        questCollider.radius = radius;
        questCollider.isTrigger = true;
        questZone.transform.position = new Vector2(x, y);
        QuestField questField = questZone.AddComponent<QuestField>();
        questField.Init(questManager);
    }

    /// <summary>
    /// ����� ���������� ������
    /// </summary>
    public void CompleteQuest()
    {
        OnCompleted?.Invoke();
        questManager.CompleteQuest();
    }
}
