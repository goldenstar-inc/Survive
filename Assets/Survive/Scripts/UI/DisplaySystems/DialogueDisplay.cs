using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    private Button closeButton;
    private Button nextLineButton;
    private TextMeshProUGUI dialogueText;
    private GameObject dialoguePanel;
    private TextMeshProUGUI nameText;
    private Image portraitImage;
    private Transform choiceContainer;
    private GameObject choiceButtonPrefab;
    private DialogueManager dialogueManager;
    private int dialogueIndex;
    private bool isTyping;
    private bool isDialogueActive;
    private Sprite interactorPortrait;
    private NPCDialogue dialogueData;

    private QuestManager questManager;
    private NPC currentNpc;
    private Action callback;

    public void Init(
        Button closeButton,
        Button nextLineButton,
        TextMeshProUGUI dialogueText,
        GameObject dialoguePanel,
        TextMeshProUGUI nameText,
        Image portraitImage,
        Transform choiceContainer,
        GameObject choiceButtonPrefab,
        DialogueManager dialogueManager,
        QuestManager questManager,
        Sprite interactorPortrait
    )
    {
        this.closeButton = closeButton;
        this.nextLineButton = nextLineButton;
        this.dialogueText = dialogueText;
        this.dialoguePanel = dialoguePanel;
        this.nameText = nameText;
        this.portraitImage = portraitImage;
        this.choiceContainer = choiceContainer;
        this.choiceButtonPrefab = choiceButtonPrefab;
        this.dialogueManager = dialogueManager;
        this.questManager = questManager;
        this.interactorPortrait = interactorPortrait;

        dialogueManager.OnDialogueStarted += StartDialogue;
        dialogueManager.OnDialogueEnded += EndDialogue;
        
        closeButton.onClick.AddListener(() => dialogueManager?.EndDialogue());
        nextLineButton.onClick.AddListener(NextLine);
    }

    private void StartDialogue(NPCDialogue dialogueData, NPC currentNpc, Action callback)
    {
        this.dialogueData = dialogueData;
        this.currentNpc = currentNpc;
        this.callback = callback;

        dialogueIndex = 0;
        isDialogueActive = true;

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);
        
        ClearChoices();
        DisplayCurrentLine();
        UpdateSpeakerUI();
    }

    private void NextLine()
    {
        if (isTyping && isDialogueActive)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.DialogueLines[dialogueIndex];
            isTyping = false;
            return;
        }

        ClearChoices();

        if (dialogueData.EndDialogueLines.Length > dialogueIndex && dialogueData.EndDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice choice in dialogueData.Choices)
        {
            if (choice.dialogueIndex == dialogueIndex)
            {
                UpdateSpeakerUI();
                DisplayChoices(choice);
                return;
            }
        }

        if (++dialogueIndex < dialogueData.DialogueLines.Length)
        {
            DisplayCurrentLine();
            UpdateSpeakerUI();
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
        UpdateSpeakerUI();
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in dialogueData.DialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.TypingSpeed);
        }

        isTyping = false;

        if (dialogueData.AutoProgressLines.Length > dialogueIndex && dialogueData.AutoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.AutoProgressDelay);
            NextLine();
        }
    }

    private void DisplayChoices(DialogueChoice choice)
    {
        ClearChoices();

        for (int i = 0; i < choice.choices.Length; i++)
        {
            string choiceText = choice.choices[i];

            int nextIndex = (i < choice.nextDialogueIndexes.Length) ? choice.nextDialogueIndexes[i] : -1;

            bool isTakeQuest = (choice.isTakeQuestOption != null && i < choice.isTakeQuestOption.Length) ? choice.isTakeQuestOption[i] : false;

            CreateChoiceButton(choiceText, () => ChooseOption(nextIndex, isTakeQuest));
        }
    }


    private void CreateChoiceButton(string choiceText, UnityAction onClick)
    {
        GameObject button = Instantiate(choiceButtonPrefab, choiceContainer);
        button.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
        button.GetComponent<Button>().onClick.AddListener(onClick);
    }

    private void ChooseOption(int nextIndex, bool isQuestTaken)
    {
        if (isQuestTaken)
        {
            callback();
        }

        if (nextIndex == -1)
        {
            EndDialogue();
        }
        else
        {
            dialogueIndex = nextIndex;
            ClearChoices();
            DisplayCurrentLine();
            UpdateSpeakerUI();
        }
    }


    private void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateSpeakerUI()
    {
        Characters currentSpeaker = dialogueData.Speakers[dialogueIndex];
        switch (currentSpeaker)
        {
            case Characters.Mark:
                if (interactorPortrait != null)
                {
                    SetNPCInfo("Mark", interactorPortrait);
                }
                break;
            default:
                if (dialogueData.Portrait != null)
                {
                    SetNPCInfo(dialogueData.Name.ToString(), dialogueData.Portrait);
                }
                break;
        }
    }

    private void SetNPCInfo(string npcName, Sprite portrait)
    {
        nameText.text = npcName;
        portraitImage.sprite = portrait;
    }

    private void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);
    }

    public void OnNextButtonClick()
    {
        UpdateSpeakerUI();
        NextLine();
    }

    private void OnDestroy()
    {
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueStarted -= StartDialogue;
            dialogueManager.OnDialogueEnded -= EndDialogue;
        }
    }
}

