using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Класс, управляющий квестами
/// </summary>
public class QuestManager : MonoBehaviour
{
    public event Action OnQuestChosen;
    public event Action<IQuest> OnQuestAdded;
    public event Action<IQuest, int> OnProgressUpdated;
    public event Action<IQuest> OnQuestCompleted;
    private IQuest currentQuest;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init()
    {
        currentQuest = null;
    }

    /// <summary>
    /// Метод добавления нового квеста
    /// </summary>
    /// <param name="quest">Новый квест</param>
    public void AddQuest(IQuest quest)
    {
        currentQuest = quest;
        OnQuestAdded?.Invoke(currentQuest);
    }

    /// <summary>
    /// Метод, зажигающий событие при обновлении прогресса
    /// </summary>
    /// <param name="quest">Квест</param>
    /// <param name="currentProgress">Текущий прогресс</param>
    
    public void UpdateProgress(IQuest quest, int currentProgress)
    {
        OnProgressUpdated?.Invoke(quest, currentProgress);
    }

    /// <summary>
    /// Метод, зажигающий событие при завершении квеста
    /// </summary>
    public void CompleteQuest()
    {
        OnQuestCompleted?.Invoke(currentQuest);
        currentQuest = null;
    }

    /// <summary>
    /// Получение текущего квеста
    /// </summary>
    /// <returns>Текущий квест</returns>
    public IQuest GetCurrentQuest()
    {
        return currentQuest;
    }

    public void QuestChosen()
    {
        OnQuestChosen?.Invoke();
    }

    private void OnDestroy()
    {
        OnQuestChosen = null;
        OnQuestAdded = null;
        OnProgressUpdated = null;
        OnQuestCompleted = null;
    }
}