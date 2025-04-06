using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "Creatures/Player Setting")]
public class PlayerSetting : CreaturesData
{
    public string Name;
    public HealthComponent HealthComponent;

    public CreatureDetectorComponent CreatureDetectorComponent;

    public RunComponent RunComponent;
    public int MaxAmmo;
}
