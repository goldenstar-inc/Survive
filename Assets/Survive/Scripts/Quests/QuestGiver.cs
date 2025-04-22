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
    public IQuest GiveQuest()
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
                    IQuest quest = new ExplorationQuest(explorationQuestConfig);
                    return quest;
                }

                if (foundQuestConfig is KillQuestConfig killQuestConfig)
                {
                    IQuest quest = new KillQuest(killQuestConfig);
                    return quest;
                }
            }
        }
        return null;
    }
}