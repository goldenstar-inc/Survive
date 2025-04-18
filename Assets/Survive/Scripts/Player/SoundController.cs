using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление звуками игрока
/// </summary>
public class SoundController : MonoBehaviour
{
    /// <summary>
    /// Словарь, содержащий пару ключ-значение, где ключ - это тип звука, а значение - аудиодорожка
    /// </summary>
    public static Dictionary<SoundType, AudioClip> stateToSound = new Dictionary<SoundType, AudioClip>();
    
    /// <summary>
    /// Массив аудиодорожек
    /// </summary>
    public AudioClip[] sounds;

    /// <summary>
    /// Источник воспроизведения звука
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Экземпляр класса Random
    /// </summary>
    private System.Random random = new System.Random();

    public void Init(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        InitializeSoundDictionary();
    }
    /// <summary>
    /// Инициализирует словарь звуков придавая каждому типу свой звук
    /// </summary>
    private void InitializeSoundDictionary()
    {
        foreach (AudioClip clip in sounds)
        {
            if (System.Enum.TryParse(clip.name, out SoundType soundType))
            {
                stateToSound[soundType] = clip;
            }
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
            audioSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Проигрывает случайный звук шагов
    /// </summary>
    public void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length == 0)
        {
            Debug.LogError("Массив пуст!");
            return;
        }
        int index = random.Next(clips.Length);
        PlayAudioClip(clips[index]);
    }
}
