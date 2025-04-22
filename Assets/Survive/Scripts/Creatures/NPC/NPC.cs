using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using System.Linq;
using System.Security.Cryptography;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] QuestGiver questGiver;
    [SerializeField] NPCDialogue dialogueData;

    public event Action<IQuest> OnQuestGiven;

    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor is IDialogueProvider dialogueProvider && interactor is IQuestProvider questProvider)
        {
            DialogueManager dialogueManager = dialogueProvider.DialogueManager;
            QuestManager questManager = questProvider.QuestManager;
            questManager.OnQuestChosen += GiveQuest;
            OnQuestGiven += questManager.AddQuest;
            dialogueManager.OnDialogueEnded += () => questManager.OnQuestChosen -= GiveQuest;
            dialogueManager.StartDialogue(dialogueData, this);
            return true;
        }

        return false;
    }
    private void GiveQuest()
    {
        if (AreAnyQuestsAvailable(out IQuest availableQuest))
        {
            OnQuestGiven?.Invoke(availableQuest);
        }
    }

    private bool AreAnyQuestsAvailable(out IQuest availableQuest)
    {
        availableQuest = questGiver.GiveQuest();
        return availableQuest != null;
    }

    private void OnDestroy()
    {
        if (questManager != null)
    {
        questManager.OnQuestChosen -= GiveQuest;
    }
        OnQuestGiven = null;
    }
}