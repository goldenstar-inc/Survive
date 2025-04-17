using System;
using UnityEngine;

/// <summary>
/// �����, �������������� �����, ��������� � ��������� ���������
/// </summary>
[CreateAssetMenu(fileName = "KillQuest", menuName = "Quests/Kill Quest")]
public class KillQuest : Quest
{
    [SerializeField] private CreatureType questTargetType;
    public override event Action OnQuestCompleted;
    private int currentQuantity;
    private QuestManager questManager;
    private WeaponManager weaponManager;

    /// <summary>
    /// �������������
    /// </summary>
    /// <param name="questManager">������, ����������� ��������</param>
    /// <param name="inventoryController">������, ����������� ����������</param>
    public void Init(QuestManager questManager, WeaponManager weaponManager)
    {
        currentQuantity = 0;
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

        if (currentQuantity == MaxProgress)
        {
            CompleteQuest();
        }
    }

    /// <summary>
    /// ����� ���������� ������
    /// </summary>
    private void CompleteQuest()
    {
        OnQuestCompleted?.Invoke();
        questManager.CompleteQuest();
        Dispose();
    }

    /// <summary>
    /// ����������� �������
    /// </summary>
    public override void Dispose()
    {
        if (weaponManager != null)
        {
            weaponManager.OnKill -= UpdateProgress;
        }
    }
}