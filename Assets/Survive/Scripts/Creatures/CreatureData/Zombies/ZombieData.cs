using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Creatures/Zombie Data")]
public class ZombieData : CreaturesData
{
    public HealthComponent HealthComponent;

    public DamageComponent DamageComponent;

    public CreatureDetectorComponent CreatureDetectorComponent;
}
