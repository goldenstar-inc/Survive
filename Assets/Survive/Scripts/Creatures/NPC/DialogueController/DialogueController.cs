using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    // private TextMeshProUGUI dialogueText;
    // private GameObject dialoguePanel;
    // private TextMeshProUGUI nameText;
    // private Image portraitImage;
    // private Transform choiceContainer;
    // private GameObject choiceButtonPrefab;
    // private NPC currentNPC;

    // public void Init(
    //     GameObject dialoguePanel,
    //     TextMeshProUGUI dialogueText,
    //     TextMeshProUGUI nameText,
    //     Image portraitImage,
    //     Transform choiceContainer,
    //     GameObject choiceButtonPrefab
    // )
    // {
    //     this.dialoguePanel = dialoguePanel;
    //     this.dialogueText = dialogueText;
    //     this.nameText = nameText;
    //     this.portraitImage = portraitImage;
    //     this.choiceContainer = choiceContainer;
    //     this.choiceButtonPrefab = choiceButtonPrefab;
    // }

    // public void ShowDialogueUI(bool show)
    // {
    //     dialoguePanel.SetActive(show);
    // }

    // public void SetNPCInfo(string npcName, Sprite portrait)
    // {
    //     nameText.text = npcName;
    //     portraitImage.sprite = portrait;
    // }

    // public void SetDialogueText(string text)
    // {
    //     dialogueText.text = text;
    // }

    // public void ClearChoices()
    // {
    //     foreach (Transform child in choiceContainer) 
    //     {
    //         Destroy(child?.gameObject);
    //     }
    // }

    // public void CreateChoiceButton(string choiceText, UnityAction onClick)
    // {
    //     GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
    //     choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
    //     choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    // }

    // public void Close()
    // {
    //     currentNPC?.EndDialogue();
    // }

    // public void Next()
    // {
    //     currentNPC?.OnNextButtonClick();
    // }

    // public void SetDialogue(NPC currentNPC)
    // {
    //     this.currentNPC = currentNPC;
    // }
}
