using UnityEngine;

public class QuestField : MonoBehaviour
{
    [SerializeField] ExplorationQuest explorationQuest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerDataProvider>(out PlayerDataProvider playerDataProvider))
        {
            if (playerDataProvider.QuestManager.GetCurrentQuest() is ExplorationQuest explorationQuest)
            {
                if(explorationQuest == this.explorationQuest)
                {
                    explorationQuest.CompleteQuest();
                    Destroy(gameObject);
                }
            }
        }
    }
}
