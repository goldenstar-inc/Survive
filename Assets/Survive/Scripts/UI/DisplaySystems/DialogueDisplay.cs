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

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="dialogueText">Текст для отображения диалога</param>
    /// <param name="dialoguePanel">Панель диалога</param>
    /// <param name="nameText">Текст для отображения имени</param>
    /// <param name="portraitImage">Изображение для вывода портрета говорящего</param>
    /// <param name="choiceContainer">Контейнер для кнопок ответа</param>
    /// <param name="choiceButtonPrefab">Префаб кнопки ответа</param>
    /// <param name="dialogueManager">Скрипт, управляющий диалогами</param>
    public void Init(
        Button closeButton,
        Button nextLineButton,
        TextMeshProUGUI dialogueText,
        GameObject dialoguePanel,
        TextMeshProUGUI nameText,
        Image portraitImage,
        Transform choiceContainer,
        GameObject choiceButtonPrefab,
        DialogueManager dialogueManager
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

        dialogueManager.OnDialogueStarted += StartDialogue;
        dialogueManager.OnDialogueEnded += EndDialogue;

        closeButton.onClick.AddListener(EndDialogue);
        nextLineButton.onClick.AddListener(NextLine);
    }
    private void NextLine()
    {
        if (isTyping && isDialogueActive)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.DialogueLines[dialogueIndex];
            isTyping = false;
        }

        ClearChoices();

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

    private void ClearChoices()
    {
        foreach (Transform child in choiceContainer) 
        {
            Destroy(child?.gameObject);
        }
    }
    private void StartDialogue(NPCDialogue dialogueData)
    {
        this.dialogueData = dialogueData;

        if (dialogueData == null)
        {
            Debug.LogError("DialogueData not loaded");
            return;
        }

        isDialogueActive = true;
        dialogueIndex = 0;
        SetNPCInfo(dialogueData.name, dialogueData.Portrait);
        UpdateSpeakerUI();
        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);
        DisplayCurrentLine();
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;

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
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }
    private void CreateChoiceButton(string choiceText, UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    }
    private void ChooseOption(int nextIndex)
    {
        dialogueIndex = nextIndex;
        ClearChoices();
        DisplayCurrentLine();
        UpdateSpeakerUI();
    }

    private void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
        UpdateSpeakerUI();
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
        dialogueText.text = string.Empty;
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
