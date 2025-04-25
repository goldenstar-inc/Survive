using UnityEngine;

[CreateAssetMenu(fileName = "ResqueQuestConfig", menuName = "Quests/Resque Quest Config")]
public class ResqueQuestConfig : QuestConfig
{
    [SerializeField] public GameObject ResqueNPC;
    [SerializeField] public Vector3 Coords;
    [SerializeField] public GameObject EnemyPrefab;
}
