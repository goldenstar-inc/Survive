using System;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public event Action<CreatureState> OnStateChanged;
    private CreatureState playerState;
    public void Init()
    {
        SetState(CreatureState.Normal);
    }
    public void SetState(CreatureState newState)
    {
        playerState = newState;
        OnStateChanged?.Invoke(playerState);
    }
}
