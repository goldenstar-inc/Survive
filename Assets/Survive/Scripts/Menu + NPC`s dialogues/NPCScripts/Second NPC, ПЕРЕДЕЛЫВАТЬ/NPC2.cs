using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HelpPhrasesModule;

public class NPC2 : MonoBehaviour, IInteractable
{
    public NPC npc;
    public NPCDialogueJoe dialogueData;
    private DialogueController dialogueUI;
    public Button nextButtonJoe; // ������ �� ������ "�����"
    public GameObject gameOver;

    private int dialogueIndexJoe;
    private bool isTyping, isDialogueActive;

    public string helpPhrase => actionToPhrase[Action.PickUp]; // ���������� ���������� IInteractable

    private void Start()
    {
        dialogueUI = DialogueController.Instance;
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public bool Interact(PlayerDataProvider interactor)
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive) || gameOver.activeSelf)
            return false;

        if (isDialogueActive)
        {
            UpdateSpeakerUI();
            NextLine();
        }
        else
        {
            UpdateSpeakerUI();
            StartDialogue();
        }

        return true;
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLinesJoe[dialogueIndexJoe]);
            isTyping = false;
        }

        dialogueUI.ClearChoices();

        if (dialogueData.endDialogueLinesJoe.Length > dialogueIndexJoe && dialogueData.endDialogueLinesJoe[dialogueIndexJoe])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoiceJoe dialogueChoice in dialogueData.choicesJoe)
        {
            if (dialogueChoice.dialogueIndexJoe == dialogueIndexJoe)
            {
                UpdateSpeakerUI();
                DisplayChoices(dialogueChoice);
                return;
            }
        }
        if (++dialogueIndexJoe < dialogueData.dialogueLinesJoe.Length)
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
        isDialogueActive = true;
        dialogueIndexJoe = 0;

        dialogueUI.SetNPCInfo(dialogueData.name, dialogueData.JoePortrait);
        UpdateSpeakerUI();
        dialogueUI.ShowDialogueUI(true);
        nextButtonJoe.gameObject.SetActive(true);


        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLinesJoe[dialogueIndexJoe])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text + letter);
            yield return new WaitForSeconds(dialogueData.typingSpeedJoe);
        }

        isTyping = false;

        if (dialogueData.autoProgressLinesJoe.Length > dialogueIndexJoe && dialogueData.autoProgressLinesJoe[dialogueIndexJoe])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelayJoe);
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoiceJoe choiceJoe)
    {
        for (int i = 0; i < choiceJoe.choicesJoe.Length; i++)
        {
            int nextIndex = choiceJoe.nextDialogueIndexesJoe[i];
            dialogueUI.CreateChoiceButton(choiceJoe.choicesJoe[i], () => ChooseOption(nextIndex));

        }
    }

    void ChooseOption(int nextIndex)
    {
        dialogueIndexJoe = nextIndex;
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
        NPCDialogueJoe.Speaker currentSpeaker = dialogueData.speakersJoe[dialogueIndexJoe];
        switch (currentSpeaker)
        {
            case NPCDialogueJoe.Speaker.Joe:
                if (dialogueData.JoePortrait != null)
                {
                    dialogueUI.SetNPCInfo("Joe", dialogueData.JoePortrait);
                }
                break;
            case NPCDialogueJoe.Speaker.Mark:
                if (dialogueData.MarkPortraitJoe != null)
                {
                    dialogueUI.SetNPCInfo("Mark", dialogueData.MarkPortraitJoe);
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
        nextButtonJoe.gameObject.SetActive(false);
        PauseController.SetPause(false);
    }

    public void OnNextButtonClick()
    {
        UpdateSpeakerUI();
        NextLine();
    }
}

