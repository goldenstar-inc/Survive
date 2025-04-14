using UnityEngine;

[CreateAssetMenu(fileName = "MovementComponent", menuName = "Components/Move")]
public class MovementComponent : ScriptableObject
{
    public int WalkSpeed;
    public AudioClip[] StepSounds;
    public float StepInterval;
}
