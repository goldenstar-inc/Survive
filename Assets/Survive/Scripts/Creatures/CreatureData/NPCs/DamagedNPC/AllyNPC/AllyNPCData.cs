using UnityEngine;

[CreateAssetMenu(fileName = "AllyNPCData", menuName = "Creatures/Ally NPC Data")]
public class AllyNPCData : DamagedNPCData
{
    public DamageComponent DamageComponent;

    public RunComponent RunComponent;
}
