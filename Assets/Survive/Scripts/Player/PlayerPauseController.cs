using System;
using UnityEngine;

public class PlayerPauseController : MonoBehaviour
{
    public event Action OnPause;
    public event Action OnResume;

    public void Pause()
    {
        OnPause.Invoke();
    }

    public void Resume()
    {
        OnResume.Invoke();
    }
}
