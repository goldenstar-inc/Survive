using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event Action<NPCDialogue> OnDialogueStarted;
    public event Action OnDialogueEnded;
    public void Init() { }
    public void StartDialogue(NPCDialogue dialogueData)
    {
        OnDialogueStarted?.Invoke(dialogueData);
    }
}
