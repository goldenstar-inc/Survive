using UnityEngine;

public class SoundController : MonoBehaviour, IDamageObserver
{
    private AudioSource audioSource;
    public AudioClip hurtSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHurtSound()
    {
        if (audioSource != null && hurtSound != null)
        {
            audioSource.clip = hurtSound;
            audioSource.Play();
        }
    }

    public void OnDamageTaken(int currentHealth, int maxHealth)
    {
        if (currentHealth != maxHealth)
        {
            PlayHurtSound();
        }
    }
}
