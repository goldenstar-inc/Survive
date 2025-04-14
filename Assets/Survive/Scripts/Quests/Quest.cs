using System;
using UnityEngine;

public abstract class Quest : ScriptableObject, IDisposable
{
    public abstract event Action OnQuestCompleted;

    public abstract void Dispose();
}