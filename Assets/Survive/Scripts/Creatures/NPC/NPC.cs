using System;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] IntreractableData data;
    [SerializeField] QuestGiver questGiver;
    [SerializeField] NPCDialogue dialogueData;
    public event Action OnInteract;
    public IntreractableData Data => data;

    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor is IDialogueProvider dialogueProvider && interactor is IQuestProvider questProvider)
        {
            OnInteract?.Invoke();
            DialogueManager dialogueManager = dialogueProvider.DialogueManager;
            QuestManager questManager = questProvider.QuestManager;
            QuestEvents questEvents = questManager?.questEvents;
            Action giveQuestCallback = () => GiveQuest(interactor, questEvents);
            dialogueManager.StartDialogue(dialogueData, this, giveQuestCallback);
            return true;
        }

        return false;
    }
    
    private void GiveQuest(PlayerDataProvider data, QuestEvents playerEvents)
    {
        if (AreAnyQuestsAvailable(out IQuest availableQuest, data, playerEvents))
        {
            if (data is IQuestProvider questProvider)
            {
                QuestManager questManager = questProvider.QuestManager;
                questManager.AddQuest(availableQuest);
            }
        }
    }

    private bool AreAnyQuestsAvailable(out IQuest availableQuest, PlayerDataProvider data, QuestEvents playerEvents)
    {
        availableQuest = questGiver.GiveQuest(data, playerEvents);
        return availableQuest != null;
    }
}