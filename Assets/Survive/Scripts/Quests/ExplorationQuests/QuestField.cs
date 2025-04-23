using UnityEngine;

public class QuestField : MonoBehaviour
{
    private ExplorationQuest fieldQuest;

    public void Init(ExplorationQuest fieldQuest)
    {
        this.fieldQuest = fieldQuest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent(out QuestManager questManager))
            {
                if (questManager.GetCurrentQuest() == fieldQuest)
                {
                    fieldQuest.UpdateProgress();
                    Destroy(gameObject);
                }
            }
        }
    }
}
