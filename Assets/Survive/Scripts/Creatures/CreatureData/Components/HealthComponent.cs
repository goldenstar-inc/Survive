using UnityEngine;

[CreateAssetMenu(fileName = "HealthComponent", menuName = "Components/Health")]
public class HealthComponent : ScriptableObject
{
    public int MaxHealth;

    public AudioClip DamageSound;

    public float InvincibilityCooldown;
}
