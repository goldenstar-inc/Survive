using UnityEngine;

[CreateAssetMenu(fileName = "MovementComponent", menuName = "Components/Move")]
public class MovementComponent : ScriptableObject
{
    public float WalkSpeed;
    public AudioClip[] StepSounds;
    public float StepInterval;
}
