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

    private DialogueController dialogueUI => DialogueController.Instance;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private Sprite interactorPortrait;

    public bool Interact(PlayerDataProvider interactor)
    {
        if (dialogueData == null)
        {
            return false;
        }

        CheckForAvailableQuests(interactor);

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
        interactorPortrait = interactor.PlayerSetting.Portrait;
        UpdateSpeakerUI();
        return true;
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

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.DialogueLines[dialogueIndex]);
            isTyping = false;
        }

        dialogueUI.ClearChoices();

        if (dialogueData.EndDialogueLines.Length > dialogueIndex && dialogueData.EndDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in dialogueData.Choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                UpdateSpeakerUI();
                DisplayChoices(dialogueChoice);
                return;
            }
        }
        if (++dialogueIndex < dialogueData.DialogueLines.Length)
        {
            UpdateSpeakerUI();
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        DialogueController.Instance.SetDialogue(this);
        isDialogueActive = true;
        dialogueIndex = 0;
        dialogueUI.SetNPCInfo(dialogueData.name, dialogueData.Portrait);
        UpdateSpeakerUI();
        dialogueUI.ShowDialogueUI(true);
        PauseController.SetPause(true);
        DisplayCurrentLine();
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.DialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text + letter);
            yield return new WaitForSeconds(dialogueData.TypingSpeed);
        }

        isTyping = false;

        if (dialogueData.AutoProgressLines.Length > dialogueIndex && dialogueData.AutoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.AutoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i=0; i< choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            dialogueUI.CreateChoiceButton(choice.choices[i],()=> ChooseOption(nextIndex));

        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();
        UpdateSpeakerUI();
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
        UpdateSpeakerUI();
    }

    void UpdateSpeakerUI()
    {
        Characters currentSpeaker = dialogueData.Speakers[dialogueIndex];
        switch (currentSpeaker)
        {
            case Characters.Antonio:
                if (dialogueData.Portrait != null)
                {
                    dialogueUI.SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
            case Characters.Cody:
                if (dialogueData.Portrait != null)
                {
                    dialogueUI.SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
            case Characters.Jacques:
                if (dialogueData.Portrait != null)
                {
                    dialogueUI.SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
            case Characters.Alice:
                if (dialogueData.Portrait != null)
                {
                    dialogueUI.SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
            case Characters.Joe:
                if (dialogueData.Portrait != null)
                {
                    dialogueUI.SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
            case Characters.Brian:
                if (dialogueData.Portrait != null)
                {
                    dialogueUI.SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
            case Characters.Mark:
                if (interactorPortrait != null)
                {
                    dialogueUI.SetNPCInfo("Mark", interactorPortrait);
                }
                break;
          
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        //nextButton.gameObject.SetActive(false);
        PauseController.SetPause(false);
    }

    public void OnNextButtonClick()
    {
        UpdateSpeakerUI();
        NextLine();
    }
}