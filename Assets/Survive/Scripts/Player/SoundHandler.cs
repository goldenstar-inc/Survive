using UnityEngine;
using static SoundController;
public class SoundHandler : MonoBehaviour
{
    [SerializeField] private float stepInterval = 0.3f;
    [SerializeField] private IPlayerDataProvider playerData;
    private float timeSinceLastStep = 0f;

    public void PlayStepSoundIfNeeded(Vector3 movement)
    {
        if (movement != Vector3.zero && Time.time - timeSinceLastStep > stepInterval)
        {
            playerData.SoundController?.PlayRandomSound(stepSounds);
            timeSinceLastStep = Time.time;
        }
    }
}
