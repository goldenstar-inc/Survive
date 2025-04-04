using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление звуками игрока
/// </summary>
public class SoundController : MonoBehaviour
{
    /// <summary>
    /// Текущий экземпляр класса SoundController
    /// </summary>
    public static SoundController Instance { get; private set; }
    
    /// <summary>
    /// Словарь, содержащий пару ключ-значение, где ключ - это тип звука, а значение - аудиодорожка
    /// </summary>
    public Dictionary<SoundType, AudioClip> stateToSound = new Dictionary<SoundType, AudioClip>();
    
    /// <summary>
    /// Массив аудиодорожек
    /// </summary>
    public AudioClip[] sounds;

    /// <summary>
    /// Массив звуков шагов
    /// </summary>
    public AudioClip[] stepSounds;

    /// <summary>
    /// Массив звуков размахивания холодным оружием
    /// </summary>
    public AudioClip[] swingingKnifeSounds;

    /// <summary>
    /// Экземпляр класса Random
    /// </summary>
    private System.Random random = new System.Random();
    
    /// <summary>
    /// Компонент AudioSource для звуков шагов
    /// </summary>
    public AudioSource footstepsAudioSource;

    /// <summary>
    /// Компонент AudioSource для оружия
    /// </summary>
    public AudioSource weaponAudioSource;

    /// <summary>
    /// Компонент AudioSource для инвентаря
    /// </summary>
    public AudioSource inventoryAudioSource;

    /// <summary>
    /// Компонент AudioSource для состояния игрока
    /// </summary>
    public AudioSource playerStateAudioSource;

    /// <summary>
    /// Компонент AudioSource для дилера
    /// </summary>
    public AudioSource dealerAudioSource;

    /// <summary>
    /// Компонент AudioSource для дилера
    /// </summary>
    public AudioSource errorAudioSource;

    /// <summary>
    /// Метод, который вызывается во время загрузки экземпляра сценария
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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
    
    public void PlaySound(SoundType soundType, AudioSource audioSource)
    {
        if (stateToSound.TryGetValue(soundType, out AudioClip clip))
        {
            PlayAudioClip(clip, audioSource);
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
    private void PlayAudioClip(AudioClip clip, AudioSource audioSource)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Проигрывает случайный звук шагов
    /// </summary>
    public void PlayRandomStepSound()
    {
        if (stepSounds.Length == 0)
        {
            Debug.LogError("Массив stepSounds пуст!");
            return;
        }

        int index = random.Next(stepSounds.Length);
        PlayAudioClip(stepSounds[index], footstepsAudioSource);
    }

    /// <summary>
    /// Проигрывает случайный звук шагов
    /// </summary>
    public void PlayRandomSwingingKnifeSound()
    {
        if (swingingKnifeSounds.Length == 0)
        {
            Debug.LogError("Массив swingingKnifeSounds пуст!");
            return;
        }

        int index = random.Next(swingingKnifeSounds.Length);
        PlayAudioClip(swingingKnifeSounds[index], weaponAudioSource);
    }
}

/// <summary>
/// Enum, содержащий все типы звуков
/// </summary>
public enum SoundType
{
    BeingDamaged,
    BeingHealed,
    Select,
    PickUp,
    Drop,
    Steps,
    ShotgunShot,
    PistolShot,
    Coke,
    Medicine,
    EmptyMag,
    NotReady,
    Bullet,
    Money,
    DealerSpeech,
    DamagedZombie
}