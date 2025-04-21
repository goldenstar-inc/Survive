using System;
using UnityEngine;

/// <summary>
/// �����, �������������� �����, ��������� � ��������� ���������
/// </summary>
public class KillQuest : IQuest, IDisposable
{
    public KillQuestConfig questConfig;

    private int currentQuantity;
    private QuestManager questManager;
    private WeaponManager weaponManager;
    public event Action OnCompleted;

    private CreatureType questTargetType => questConfig.QuestTargetType;
    private int maxProgress => questConfig.MaxProgress;
    public QuestConfig QuestConfig => questConfig;


    /// <summary>
    /// �������������
    /// </summary>
    /// <param name="questManager">������, ����������� ��������</param>
    /// <param name="weaponManager">������, ����������� ������</param>
    public KillQuest(KillQuestConfig questConfig, QuestManager questManager, WeaponManager weaponManager)
    {
        currentQuantity = 0;
        this.questConfig = questConfig;
        this.questManager = questManager;
        this.weaponManager = weaponManager;
        this.weaponManager.OnKill += UpdateProgress;
    }

    /// <summary>
    /// ���������� ���������
    /// </summary>
    /// <param name="index">������</param>
    /// <param name="quantity">���������� ������������ ��������</param>
    /// <param name="data">������ � ����������� ��������</param>
    private void UpdateProgress(CreatureType enemyType)
    {
        if (questTargetType == enemyType)
        {
            currentQuantity += 1;
            questManager?.UpdateProgress(this, currentQuantity);
        }

        if (currentQuantity == maxProgress)
        {
            CompleteQuest();
        }
    }

    /// <summary>
    /// ����� ���������� ������
    /// </summary>
    public void CompleteQuest()
    {
        OnCompleted?.Invoke();
        questManager.CompleteQuest();
        Dispose();
    }

    /// <summary>
    /// ����������� �������
    /// </summary>
    public void Dispose()
    {
        if (weaponManager != null)
        {
            weaponManager.OnKill -= UpdateProgress;
        }
    }
}