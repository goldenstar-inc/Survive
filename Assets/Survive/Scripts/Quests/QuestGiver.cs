using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestGiver : MonoBehaviour 
{
    [SerializeField] private List<QuestConfig> quests;
    /*public void Init(List<QuestConfig> quests)
    {
        this.quests = quests;
    }*/
    public IQuest GiveQuest(PlayerDataProvider interactor)
    {
        if (quests != null)
        {
            if (quests.Count > 0)
            {
                QuestConfig foundQuestConfig = quests.First();
                quests.Remove(foundQuestConfig);

                if (foundQuestConfig is DeliveryQuestConfig deliveryQuestConfig)
                {
                    IQuest quest = new DeliveryQuest(deliveryQuestConfig);
                    return quest;
                }
                
                if (foundQuestConfig is ExplorationQuestConfig explorationQuestConfig)
                {
                    QuestManager questManager = interactor.QuestManager;
                    IQuest quest = new ExplorationQuest(explorationQuestConfig, questManager);
                    return quest;
                }

                if (foundQuestConfig is KillQuestConfig killQuestConfig)
                {
                    QuestManager questManager = interactor.QuestManager;
                    WeaponManager weaponManager = interactor.WeaponManager;
                    IQuest quest = new KillQuest(killQuestConfig, questManager, weaponManager);
                    return quest;
                }
            }
        }
        return null;
    }
}