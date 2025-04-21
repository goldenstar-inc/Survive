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

    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor is IDialogueProvider dialogueProvider && interactor is IQuestProvider questProvider)
        {
            DialogueManager dialogueManager = dialogueProvider.DialogueManager;
            QuestManager questManager = questProvider.QuestManager;
            questManager.OnQuestChosen += GiveQuest;
            dialogueManager.OnDialogueEnded += () => questManager.OnQuestChosen -= GiveQuest;
            dialogueManager.StartDialogue(dialogueData, this);
            return true;
        }

        return false;
    }
    private void GiveQuest(PlayerDataProvider interactor)
    {
        if (AreAnyQuestsAvailable(out IQuest availableQuest, interactor))
        {
            QuestManager questManager = interactor.QuestManager;
            questManager.AddQuest(availableQuest);
        }
    }

    private bool AreAnyQuestsAvailable(out IQuest availableQuest, PlayerDataProvider interactor)
    {
        availableQuest = questGiver.GiveQuest(interactor);
        return availableQuest != null;
    }
}