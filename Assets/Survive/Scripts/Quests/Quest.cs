using System;
using UnityEngine;

/// <summary>
/// Класс, представляющий квест
/// </summary>
public abstract class Quest : ScriptableObject, IDisposable
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField, Range(1, 100)] public int MaxProgress;
    [SerializeField] public AudioClip QuestComplete;
    public abstract event Action OnQuestCompleted;
    public abstract void Dispose();
}