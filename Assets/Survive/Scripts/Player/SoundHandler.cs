using UnityEngine;
public class SoundHandler : MonoBehaviour
{
    [SerializeField] PlayerSetting setting;
    [SerializeField] SoundController soundController;
    private AudioClip[] stepSounds => setting.RunComponent.StepSounds;
    private float stepInterval => setting.RunComponent.StepInterval;
    private float timeSinceLastStep = 0f;

    public void Initialize(AudioClip[] stepSounds, float stepInterval, SoundController soundController)
    {
        this.stepSounds = stepSounds;
        this.stepInterval = stepInterval;
        this.soundController = soundController;
    }

    public void PlayStepSoundIfNeeded(Vector3 movement)
    {
        if (movement != Vector3.zero && Time.time - timeSinceLastStep > stepInterval)
        {
            soundController?.PlayRandomSound(stepSounds);
            timeSinceLastStep = Time.time;
        }
    }
}
