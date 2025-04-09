using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static HelpPhrasesModule;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    private DialogueController dialogueUI;
    public Button nextButton; // ������ �� ������ "�����"
    public GameObject gameOver;
    private int dialogueIndex;
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
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }

        dialogueUI.ClearChoices();

        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                UpdateSpeakerUI();
                DisplayChoices(dialogueChoice);
                return;
            }
        }
        if (++dialogueIndex < dialogueData.dialogueLines.Length)
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
        dialogueIndex = 0;

        dialogueUI.SetNPCInfo(dialogueData.name, dialogueData.BrianPortrait);
        UpdateSpeakerUI();
        dialogueUI.ShowDialogueUI(true);
        nextButton.gameObject.SetActive(true);
        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text + letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
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
        NPCDialogue.Speaker currentSpeaker = dialogueData.speakers[dialogueIndex];
        switch (currentSpeaker)
        {
            case NPCDialogue.Speaker.Brian:
                if (dialogueData.BrianPortrait != null)
                {
                    dialogueUI.SetNPCInfo("Brian", dialogueData.BrianPortrait);
                }
                break;
            case NPCDialogue.Speaker.Mark:
                if (dialogueData.MarkPortrait != null)
                {
                    dialogueUI.SetNPCInfo("Mark", dialogueData.MarkPortrait);
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
        nextButton.gameObject.SetActive(false);
        PauseController.SetPause(false);
    }

    public void OnNextButtonClick()
    {
        UpdateSpeakerUI();
        NextLine();
    }
}