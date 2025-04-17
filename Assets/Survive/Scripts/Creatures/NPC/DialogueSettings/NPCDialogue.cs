using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [SerializeField] public Characters Name;
    [SerializeField] public Sprite Portrait;
    [SerializeField] public string[] DialogueLines;
    [SerializeField] public Characters[] Speakers;
    [SerializeField] public bool[] AutoProgressLines;
    [SerializeField] public bool[] EndDialogueLines;
    [SerializeField, Range(0,10)] public float AutoProgressDelay = 1.5f;
    [SerializeField, Range(0,1)] public float TypingSpeed = 0.05f;
    [SerializeField] public AudioClip VoiceSound;
    [SerializeField, Range(0,10)] public float VoicePitch = 1f;
    [SerializeField] public DialogueChoice[] Choices;
}

