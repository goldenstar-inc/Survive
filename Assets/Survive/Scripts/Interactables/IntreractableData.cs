using UnityEngine;

[CreateAssetMenu(fileName = "IntreractableData", menuName = "Intreractables/Intreractable Data")]
public class IntreractableData : ScriptableObject
{
    [SerializeField] public AudioClip[] InteractionSounds;
}
