using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event Action<NPCDialogue, NPC> OnDialogueStarted;
    public event Action OnDialogueEnded;
    public void Init() { }
    public void StartDialogue(NPCDialogue dialogueData, NPC npc)
    {
        OnDialogueStarted?.Invoke(dialogueData, npc);
    }
}
