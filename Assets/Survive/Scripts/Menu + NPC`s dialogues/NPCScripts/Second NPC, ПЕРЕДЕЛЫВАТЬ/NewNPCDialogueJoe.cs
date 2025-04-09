using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogueJoe", menuName = "NPC DialogueJoe")]
public class NPCDialogueJoe : ScriptableObject
{
    public enum Speaker { Mark, Joe } // добавил

    public string nameOfNPC;
    public Sprite JoePortrait;
    public Sprite MarkPortraitJoe; // добавил
    public string[] dialogueLinesJoe;
    public Speaker[] speakersJoe; // добавил
    public bool[] autoProgressLinesJoe;
    public bool[] endDialogueLinesJoe;
    public float autoProgressDelayJoe = 1.5f;
    public float typingSpeedJoe = 0.05f;
    public AudioClip voiceSoundJoe;
    public float voicePitchJoe = 1f;

    public DialogueChoiceJoe[] choicesJoe;
}

[System.Serializable]
public class DialogueChoiceJoe
{
    public int dialogueIndexJoe;
    public string[] choicesJoe;
    public int[] nextDialogueIndexesJoe;
}
