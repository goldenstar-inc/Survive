using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using System.Linq;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] NPCDialogue dialogueData;
    [SerializeField] List<Quest> activeQuests;
    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null && interactor is IDialogueProvider dialogueProvider)
        {
            CheckForAvailableQuests(interactor); 
            dialogueProvider.DialogueManager.StartDialogue(dialogueData);
            return true;
        }

        return false;
    }

    private void CheckForAvailableQuests(PlayerDataProvider interactor)
    {
        if (activeQuests.Count > 0)
        {
            Quest availableQuest = activeQuests.First(); 
            RaiseQuest(interactor, availableQuest);
            availableQuest.OnQuestCompleted += GiveReward;
            activeQuests.Remove(availableQuest);
        }
    }
    private void GiveReward()
    {
        Debug.Log("Good job!");
    }
    public void RaiseQuest(PlayerDataProvider interactor, Quest quest)
    {
        if (interactor != null)
        {
            if (interactor is IQuestProvider questProvider)
            {
                questProvider.QuestManager.AddQuest(quest);
            }
        }
    }
}