using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DeliveryQuest", menuName = "Quests/Delivery Quest")]
public class DeliveryQuest : Quest
{
    [SerializeField] private PickableItems questItem;
    [SerializeField] private int questItemAmount;

    private InventoryController inventoryController;

    public override event Action OnQuestCompleted;

    public void Init(InventoryController inventoryController)
    {

        this.inventoryController = inventoryController;

        this.inventoryController.OnPickUp += IsQuestComplete;
    }

    private void IsQuestComplete(int index, int quantity, InventoryItemData data)
    {
        if (questItem == data.Name && questItemAmount == quantity)
        {
            OnQuestCompleted?.Invoke();

            Dispose();
        }
    }

    public override void Dispose()
    {
        inventoryController.OnPickUp -= IsQuestComplete;
    }
}