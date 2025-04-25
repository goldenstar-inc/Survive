using System;
using UnityEngine;

/// <summary>
/// �����, �������������� �����
/// </summary>
public abstract class QuestConfig : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField, Range(1, 100)] public int MaxProgress;
    [SerializeField] public AudioClip QuestComplete;
}