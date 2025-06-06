using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour 
{
    [SerializeField] private List<QuestConfig> quests;
    /// <summary>
    /// �����, ���������������� ������� � NPC
    /// </summary>
    /// <param name="quests">������ �������</param>
    public void Init(List<QuestConfig> quests)
    {
        this.quests = quests;
    }

    /// <summary>
    /// ����� �������� �������
    /// </summary>
    /// <param name="data">������ ������</param>
    /// <param name="playerEvents">������� ��������� � ���������� �������</param>
    /// <returns>�������</returns>
    public IQuest GiveQuest(PlayerDataProvider data, QuestEvents playerEvents)
    {
        if (quests != null)
        {
            if (quests.Count > 0)
            {
                QuestConfig foundQuestConfig = quests.First();
                quests.Remove(foundQuestConfig);

                if (foundQuestConfig is DeliveryQuestConfig deliveryQuestConfig)
                {
                    IQuest quest = new DeliveryQuest(deliveryQuestConfig, data, playerEvents);
                    return quest;
                }
                
                if (foundQuestConfig is ExplorationQuestConfig explorationQuestConfig)
                {
                    if (data is IQuestProvider questProvider)
                    {
                        QuestManager questManager = questProvider.QuestManager;
                        IQuest quest = new ExplorationQuest(explorationQuestConfig, questManager);
                        return quest;
                    }
                }

                if (foundQuestConfig is KillQuestConfig killQuestConfig)
                {
                    if (data is IQuestProvider questProvider && data is IWeaponProvider weaponProvider)
                    {
                        QuestManager questManager = questProvider.QuestManager;
                        WeaponManager weaponManager = weaponProvider.WeaponManager;
                        IQuest quest = new KillQuest(killQuestConfig, questManager, weaponManager);
                        return quest;
                    }
                }

                if (foundQuestConfig is ResqueQuestConfig resqueQuestConfig)
                {
                    if (data is IQuestProvider questProvider && data is IWeaponProvider weaponProvider)
                    {
                        QuestManager questManager = questProvider.QuestManager;
                        IQuest quest = new ResqueQuest(resqueQuestConfig, questManager, playerEvents);
                        return quest;
                    }
                }
            }
        }
        return null;
    }
}