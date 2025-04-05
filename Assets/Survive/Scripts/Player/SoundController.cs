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
    /// Массив звуков шагов
    /// </summary>
    public static AudioClip[] stepSounds;

    /// <summary>
    /// Массив звуков размахивания холодным оружием
    /// </summary>
    public static AudioClip[] swingingKnifeSounds;

    /// <summary>
    /// Источник воспроизведения звука
    /// </summary>
    [SerializeField] AudioSource audioSource;

    /// <summary>
    /// Экземпляр класса Random
    /// </summary>
    private System.Random random = new System.Random();

    /// <summary>
    /// Метод, который вызывается во время загрузки экземпляра сценария
    /// </summary>
    void Awake()
    {
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
            else
            {
                Debug.LogWarning($"Звук {clip.name} не соответствует ни одному значению в SoundType.");
            }
        }
    }

    /// <summary>
    /// Метод, проигрывающий звук, если тип звука имеет аудиодорожку
    /// </summary>
    /// <param name="soundType">Тип звука</param>
    
    public void PlaySound(SoundType soundType)
    {
        if (stateToSound.TryGetValue(soundType, out AudioClip clip))
        {
            PlayAudioClip(clip);
        }
        else
        {
            Debug.LogError($"Звук для типа {soundType} не найден!");
        }
    }

    /// <summary>
    /// Функция, воспроизводящая переданный звук
    /// </summary>
    /// <param name="clip">Переданный звук</param>
    private void PlayAudioClip(AudioClip clip)
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
        if (stepSounds.Length == 0)
        {
            Debug.LogError("Массив stepSounds пуст!");
            return;
        }

        int index = random.Next(clips.Length);
        PlayAudioClip(clips[index]);
    }
}
