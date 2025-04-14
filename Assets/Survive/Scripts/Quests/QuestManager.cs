using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> activeQuests = new ();
    private InventoryController inventoryController;

    public void Init(InventoryController inventoryController)
    {
        this.inventoryController = inventoryController;

    }

    public void AddQuest(Quest quest)
    {
        if (quest is DeliveryQuest deliveryQuest)
        {
            deliveryQuest.Init(inventoryController);
            Debug.Log(deliveryQuest);
            activeQuests.Add(deliveryQuest);
        }
    }
}
