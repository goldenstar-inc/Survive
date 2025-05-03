using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за отображение взятых квестов
/// </summary>
public class QuestDisplay : MonoBehaviour
{
    private TextMeshProUGUI questNamePlaceholder;
    private TextMeshProUGUI questProgressPlaceholder;
    private QuestManager questManager;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questNamePlaceholder">Текст с названием квеста</param>
    /// <param name="questProgressPlaceholder">Текст с текущим прогрессом по квесту</param>
    /// <param name="questManager">Класс, управляющий квестами</param>
    public void Init(
        TextMeshProUGUI questNamePlaceholder,
        TextMeshProUGUI questProgressPlaceholder,
        QuestManager questManager
    )
    {
        this.questNamePlaceholder = questNamePlaceholder;
        this.questProgressPlaceholder = questProgressPlaceholder;
        this.questManager = questManager;

        if (!Validate()) return;

        questManager.OnQuestAdded += AddQuest;
        questManager.OnProgressUpdated += UpdateProgress;
        questManager.OnQuestCompleted += CompleteQuest;

        ClearQuestBar();
    }

    /// <summary>
    /// Метод добавления квеста
    /// </summary>
    /// <param name="quest">Квест</param>
    private void AddQuest(IQuest quest)
    {
        questNamePlaceholder.text = quest.QuestConfig.Name;
        questProgressPlaceholder.text = quest.QuestConfig.Description;
    }

    /// <summary>
    /// Метод, обновляющий текущий прогрессс по квесту
    /// </summary>
    /// <param name="quest"></param>
    /// <param name="currentProgress"></param>
    private void UpdateProgress(IQuest quest, int currentProgress)
    {
        questProgressPlaceholder.text = quest.QuestConfig.Description;
        questProgressPlaceholder.text += " " + $"{currentProgress} / {quest.QuestConfig.MaxProgress}";
    }

    /// <summary>
    /// Метод завершения квеста
    /// </summary>
    private void CompleteQuest(IQuest quest)
    {
        questNamePlaceholder.text = "DONE!";
        questProgressPlaceholder.text = $"<s>{questProgressPlaceholder.text}</s>";
        Invoke(nameof(ClearQuestBar), 3f);
    }

    private void ClearQuestBar()
    {
        questNamePlaceholder.text = string.Empty;
        questProgressPlaceholder.text = string.Empty;
    }

    /// <summary>
    /// Валидация параметров
    /// </summary>
    /// <returns>True - если все системы прогружены корректно, иначе - false</returns>
    private bool Validate()
    {
        if (questNamePlaceholder == null)
        {
            Debug.LogError("QuestNamePlaceholder not loaded");
            return false;
        }

        if (questProgressPlaceholder == null)
        {
            Debug.LogError("QuestProgressPlaceholder not loaded");
            return false;
        }

        if (questManager == null)
        {
            Debug.LogError("QuestManager not loaded");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Уничтожение объекта
    /// </summary>
    private void OnDisable()
    {
        if (questManager != null)
        {
            questManager.OnQuestAdded -= AddQuest;
            questManager.OnProgressUpdated -= UpdateProgress;
            questManager.OnQuestCompleted -= CompleteQuest;
        }
    }
}
