using System;
using UnityEngine;

/// <summary>
/// �����, �������������� �����
/// </summary>
public class ExplorationQuest : IQuest, IDisposable
{
    private ExplorationQuestConfig questConfig;

    public event Action OnCompleted;
    private QuestManager questManager;
    private int currentProgress;
    private int maxProgress => questConfig.MaxProgress;
    private float x => questConfig.X;
    private float y => questConfig.Y;
    private float radius => questConfig.Radius;
    public QuestConfig QuestConfig => questConfig;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questConfig">Конфиг квеста</param>
    public ExplorationQuest(ExplorationQuestConfig questConfig, QuestManager questManager)
    {
        this.questConfig = questConfig;
        this.questManager = questManager;
        OnCompleted += questManager.CompleteQuest;
        CreateQuestZone();
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
        questField.Init(this);
    }

    public void UpdateProgress()
    {
        currentProgress += 1;
        questManager?.UpdateProgress(this, currentProgress);
        if (currentProgress >= maxProgress)
        {
            CompleteQuest(this);
        }
    }

    /// <summary>
    /// ����� ���������� ������
    /// </summary>
    public void CompleteQuest(IQuest quest)
    {
        OnCompleted?.Invoke();
    }

    public void Dispose()
    {
        OnCompleted = null;
    }
}
