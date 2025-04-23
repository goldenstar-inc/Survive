using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public event Action<NPCDialogue, NPC, Action> OnDialogueStarted;
    public event Action OnDialogueEnded;
    private PlayerPauseController playerPauseController;
    public void Init(PlayerPauseController playerPauseController) 
    {
        this.playerPauseController = playerPauseController;
    }
    public void StartDialogue(NPCDialogue dialogueData, NPC npc, Action callBack)
    {
        playerPauseController.Pause();
        OnDialogueStarted?.Invoke(dialogueData, npc, callBack);
    }

    public void EndDialogue()
    {
        playerPauseController.Resume();
        OnDialogueEnded?.Invoke();
    }
    private void OnDestroy()
    {
        OnDialogueStarted = null;
        OnDialogueEnded = null;
    }
}
