using System;

public interface IQuest
{
    public QuestConfig QuestConfig { get; }
    public event Action OnCompleted;
    public void CompleteQuest();
}