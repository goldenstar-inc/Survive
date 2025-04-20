using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using System.Linq;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] QuestGiver questGiver;
    [SerializeField] NPCDialogue dialogueData;
    [SerializeField] NPC npc;
    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor is IDialogueProvider dialogueProvider)
        {
            dialogueProvider.DialogueManager.StartDialogue(dialogueData,npc); // передаём this
            return true;
        }


        return false;
    }

    public void CheckForAvailableQuests(PlayerDataProvider interactor)
    {
        IQuest availableQuest = questGiver.GiveQuest(interactor.QuestManager);
        Debug.Log(availableQuest);
        if (availableQuest != null)
        {
            RaiseQuest(interactor, availableQuest);
        }
    }
    public void RaiseQuest(PlayerDataProvider interactor, IQuest quest)
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