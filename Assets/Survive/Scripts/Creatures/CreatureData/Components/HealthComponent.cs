using UnityEngine;

[CreateAssetMenu(fileName = "HealthComponent", menuName = "Components/Health")]
public class HealthComponent : ScriptableObject
{
    public float MaxHealth;

    public AudioClip DamagedSound;

    public float InvincibilityCooldown;
}
