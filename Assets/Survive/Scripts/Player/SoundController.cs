using Random = System.Random;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering.LookDev;
using Unity.Multiplayer.Center.Common.Analytics;

/// <summary>
/// Класс, отвечающий за управление звуками игрока
/// </summary>
public class SoundController : MonoBehaviour
{
    private PlayerDataProvider playerData;
    
    /// <summary>
    /// Источник воспроизведения звука
    /// </summary>
    private AudioSource audioSource;
    private float pitchVariation = 0.05f;
    public float volumeVariation = 0.1f;
    private float lastPlayTime = 0f;
    private float playCooldown = 0.1f;

    /// <summary>
    /// Экземпляр класса Random
    /// </summary>
    private Random random = new Random();
    private HealthHandler healthHandler;
    private InventoryController inventoryController;
    private QuestManager questManager;
    private PlayerMovement movement;
    private WeaponManager weaponManager;
    private PlayerInteractionDetector interactionDetector;

    public void Init(
        AudioSource audioSource,
        PlayerDataProvider playerData,
        HealthHandler healthHandler,
        InventoryController inventoryController,
        QuestManager questManager,
        PlayerMovement movement,
        WeaponManager weaponManager,
        PlayerInteractionDetector interactionDetector
        )
    {
        this.audioSource = audioSource;
        this.playerData = playerData;
        this.healthHandler = healthHandler;
        this.inventoryController = inventoryController;
        this.questManager = questManager;
        this.movement = movement;
        this.weaponManager = weaponManager;
        this.interactionDetector = interactionDetector;

        healthHandler.OnDamageTaken += PlaySound;
        movement.OnStepTaken += PlaySound;
        inventoryController.OnSelectionChanged += PlaySelectionChangedSound;
        interactionDetector.OnInteract += PlayInteractSound;
        inventoryController.OnItemPickedUp += PlayItemPickUpSound;
        inventoryController.OnItemUsed += PlayItemUseSound;
        inventoryController.OnItemDropped += PlayItemDropSound;
        weaponManager.OnAttack += PlaySound;
        questManager.OnQuestCompleted += PlaySound;
    }
    private void PlaySound(int _, int __, HealthComponent component)
    {
        if (component != null)
        {
            AudioClip[] clips = component.DamagedSound;
            PlaySound(clips);
        }
    }
    private void PlaySound(MovementComponent component)
    {
        if (component != null)
        {
            AudioClip[] clips = component.StepSounds;
            PlaySound(clips);
        }
    }
    private void PlaySelectionChangedSound(int _, UISoundPack pack)
    {
        if (pack != null)
        {
            AudioClip[] clips = pack.InventorySelectionChanged;
            PlaySound(clips);
        }
    }
    private void PlayInteractSound(IInteractable interactable)
    {
        if (interactable != null)
        {
            IntreractableData data = interactable.Data;
            AudioClip[] clips = data.InteractionSound;
            PlaySound(clips);
        }
    }
    private void PlayItemPickUpSound(int _, int __, int ___, InventoryItemData itemData)
    {
        if (itemData != null)
        {
            AudioClip[] clips = itemData.InteractionSound;
            PlaySound(clips);
        }
    }
    private void PlayItemUseSound(int _, int __, InventoryItemData itemData)
    {
        if (itemData != null)
        {
            if (itemData is UsableItemData usableItemData)
            {
                AudioClip[] clips = usableItemData.Sound;
                PlaySound(clips);
            }
        }
    }
    private void PlayItemDropSound(int _, int __, InventoryItemData itemData)
    {
        if (itemData != null)
        {
            AudioClip[] clips = itemData.DropSound;
            PlaySound(clips);
        }
    }
    private void PlaySound(WeaponItemData itemData)
    {
        if (itemData != null)
        {
            AudioClip[] clips = itemData.AttackSound;
            PlaySound(clips);
        }
    }
    private void PlaySound(IQuest quest)
    {
        if (quest != null && quest.QuestConfig != null)
        {
            AudioClip[] clips = quest.QuestConfig.QuestComplete;
            PlaySound(clips);
        }
    }

    private void PlaySound(AudioClip[] clips)
    {
        if (clips != null)
        {
            PlayRandomSound(clips);
        }
        else
        {
            Debug.LogError("Audio clip is null");
            return;
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

    /// <summary>
    /// Функция, воспроизводящая переданный звук
    /// </summary>
    /// <param name="clip">Переданный звук</param>
    public void PlayAudioClip(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            if (Time.time - lastPlayTime >= playCooldown)
            {
                float randomVolume = UnityEngine.Random.Range(1f - volumeVariation, 1f + volumeVariation);
                audioSource.pitch = UnityEngine.Random.Range(1f - pitchVariation, 1f + pitchVariation);
                audioSource.PlayOneShot(clip, randomVolume);
                audioSource.pitch = 1f;
                lastPlayTime = Time.time;
            }
        }
    }
    private void OnDisable()
    {
        if (healthHandler != null)
        {
            healthHandler.OnDamageTaken -= PlaySound;
        }

        if (inventoryController != null)
        {
            inventoryController.OnSelectionChanged -= PlaySelectionChangedSound;
            inventoryController.OnItemPickedUp -= PlayItemPickUpSound;
            inventoryController.OnItemUsed -= PlayItemUseSound;
            inventoryController.OnItemDropped -= PlayItemDropSound;
        }

        if (questManager != null)
        {
            
        }

        if (movement != null)
        {
            movement.OnStepTaken -= PlaySound;
        }
         
        if (weaponManager != null)
        {
            weaponManager.OnAttack -= PlaySound;
        }

        if (interactionDetector != null)
        {
            interactionDetector.OnInteract -= PlayInteractSound;
        }
    }
}
