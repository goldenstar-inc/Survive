using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление звуками игрока
/// </summary>
public class SoundController : MonoBehaviour, IDamageObserver, IHealObserver
{
    public Dictionary<string, AudioClip> stateToSound = new Dictionary<string, AudioClip>();
    public AudioClip[] sounds;

    private AudioSource audioSource;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeSoundDictionary();
    }

    /// <summary>
    /// Инициализация словаря со звуками
    /// </summary>
    private void InitializeSoundDictionary()
    {
        foreach (AudioClip clip in sounds)
        {
            stateToSound.Add(clip.name, clip);
        }
    }
    
    /// <summary>
    /// Метод, проигрывающий звук получения урона игроком
    /// </summary>
    /// <param name="currentHealth">Текущее количество очков здоровья</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    public void OnDamageTaken(int currentHealth, int maxHealth)
    {
        string state = "BeingDamaged";
        AudioClip clip = stateToSound[state];
            
        if (clip != null)
        {
            PlayAudioClip(clip);
        }
    }

    /// <summary>
    /// Метод, проигрывающий звук восстановления очков здоровья игроком
    /// </summary>
    /// <param name="currentHealth">Текущее количество очков здоровья</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    public void OnHealApplied(int currentHealth, int maxHealth)
    {
        string state = "BeingHealed";
        AudioClip clip = stateToSound[state];
                
        if (clip != null)
        {
            PlayAudioClip(clip);
        }
    }
    
    /// <summary>
    /// Функция, воспроизводящая переданный звук
    /// </summary>
    /// <param name="clip">Переданный звук</param>
    public void PlayAudioClip(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
