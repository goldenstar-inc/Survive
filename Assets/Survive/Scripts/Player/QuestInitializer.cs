using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Класс, инициализирующий квесты игрока
/// </summary>
/// 
public class QuestInitializer : MonoBehaviour
{
    private PlayerDataProvider data;

    private QuestManager questManager;
    
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="data">Фасад игрока</param>
    
    public void Init(PlayerDataProvider data)
    {
        this.data = data;
        
        if (data is IQuestProvider questProvider)
        {
            QuestManager questManager = questProvider.QuestManager;
            this.questManager = questManager;

            questManager.OnQuestChosen += InitializeQuest;
        }

        Validate();
    }

    /// <summary>
    /// Валидация полей класса
    /// </summary>

    private void Validate()
    {
        if (data == null)
        {
            Debug.Log("Data not loaded");
        }

        if (questManager == null)
        {
            Debug.Log("QuestManager not loaded");
        }
    }

    /// <summary>
    /// Инициализация квеста
    /// </summary>
    /// <param name="quest">Квест для инициализации</param>

    public void InitializeQuest(IQuest quest)
    {
        if (quest is ExplorationQuest explorationQuest)
        {
            explorationQuest.SetQuestManager(questManager);
        }
    }
}
