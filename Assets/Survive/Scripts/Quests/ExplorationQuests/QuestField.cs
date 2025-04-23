using UnityEngine;

public class QuestField : MonoBehaviour
{
    private QuestManager interactor;

    public void Init(QuestManager interactor)
    {
        this.interactor = interactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.TryGetComponent(out QuestManager questManager))
            {
                if (questManager == interactor)
                {
                    questManager.CompleteQuest();
                    Destroy(gameObject);
                }
            }
        }
    }
}
