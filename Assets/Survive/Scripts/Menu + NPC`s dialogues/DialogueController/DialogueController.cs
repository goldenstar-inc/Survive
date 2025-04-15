using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    public Transform choiceContainer;
    public GameObject choiceBottonPrefab;

    private NPC currentNPC;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowDialogueUI( bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void SetNPCInfo (string npcName, Sprite portrait)
    {
        nameText.text = npcName;
        portraitImage.sprite = portrait;
    }

    public void SetDialogueText (string text)
    {
        dialogueText.text = text;
    }

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceBottonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void Close()
    {
        if (currentNPC != null)
        {
            currentNPC.EndDialogue();
        }
    }

    public void Next()
    {
        if (currentNPC != null)
        {
            currentNPC.OnNextButtonClick();
        }
    }

    internal void SetDialogue(NPC currentNPC)
    {
        if (currentNPC != null)
        {
            this.currentNPC = currentNPC;
        }
    }
}
