using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static HelpPhrasesModule;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] List<Quest> activeQuests;

    public NPCDialogue dialogueData;
    private DialogueController dialogueUI => DialogueController.Instance;
    public Button nextButton;
    public GameObject gameOver;
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private Sprite interactorPortrait;

    public string helpPhrase => actionToPhrase[Action.PickUp];

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public bool Interact(PlayerDataProvider interactor)
    {
        if (dialogueData == null/* || (PauseController.IsGamePaused && !isDialogueActive) || gameOver.activeSelf*/)
            return false;

        if (activeQuests.Count > 0)
        {
            RaiseQuest(interactor, activeQuests.First());
            activeQuests.RemoveAt(0);
        }

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
        interactorPortrait = interactor.PlayerSetting.Portrait;

        return true;
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
        //nextButton.gameObject.SetActive(true);
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