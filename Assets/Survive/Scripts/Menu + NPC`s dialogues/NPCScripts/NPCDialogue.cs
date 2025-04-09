using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public enum Speaker { Alice, Mark, Joe, Antonio, Cody, Brian } // добавил

    public string nameOfNPC;
    public Sprite BrianPortrait;
    public Sprite MarkPortrait; // добавил
    public string[] dialogueLines;
    public Speaker[] speakers; // добавил
    public bool[] autoProgressLines;
    public bool[] endDialogueLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;

    public DialogueChoice[] choices;
}

[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex;
    public string[] choices;
    public int[] nextDialogueIndexes;
}
