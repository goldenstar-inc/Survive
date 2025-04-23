using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event Action<NPCDialogue, NPC, Action> OnDialogueStarted;
    public event Action OnDialogueEnded;
    public void Init() { }
    public void StartDialogue(NPCDialogue dialogueData, NPC npc, Action callBack)
    {
        OnDialogueStarted?.Invoke(dialogueData, npc, callBack);
    }

    public void EndDialogue()
    {
        OnDialogueEnded?.Invoke();
    }

    private void OnDestroy()
    {
        OnDialogueStarted = null;
        OnDialogueEnded = null;
    }
}
