using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Creatures/NPC Data")]
public class NPCData : CreaturesData
{
    public string Name;
    public CreatureDetectorComponent CreatureDetectorComponent;
}
