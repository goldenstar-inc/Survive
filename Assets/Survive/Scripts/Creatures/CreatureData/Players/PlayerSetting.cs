using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "Creatures/Player Setting")]
public class PlayerSetting : CreaturesData
{
    [SerializeField] public string Name;
    [SerializeField] public HealthComponent HealthComponent;
    [SerializeField] public CreatureDetectorComponent CreatureDetectorComponent;
    [SerializeField] public RunComponent RunComponent;
    [SerializeField] public int MaxAmmo;
    [SerializeField] public Sprite Portrait;
}
