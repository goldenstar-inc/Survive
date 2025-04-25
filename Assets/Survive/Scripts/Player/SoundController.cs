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
    private Dictionary<SoundType, AudioClip[]> typeToSound = new ();
    
    /// <summary>
    /// Источник воспроизведения звука
    /// </summary>
    private AudioSource audioSource;

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

        InitSoundMap();

        healthHandler.OnDamageTaken += (_, _) => PlaySound(SoundType.BeingDamaged);
        movement.OnStepTaken += () => PlaySound(SoundType.Steps);
        inventoryController.OnSelectionChanged += (_) => PlaySound(SoundType.Select);
        inventoryController.OnItemUsed += (_, _, itemData) => PlayItemUseSound(itemData);
        inventoryController.OnItemDropped += (_, _, itemData) => PlayItemDropSound(itemData);
        inventoryController.OnItemPickedUp += (_, _, itemData) => PlayItemPickUpSound(itemData);
        weaponManager.OnAttack += PlayWeaponSound;
        interactionDetector.OnPickUp += PlayPickUpSound;
        interactionDetector.OnInteract += PlayInteractSound;
    }

    private void PlayWeaponSound(WeaponItemData itemData)
    {
        if (itemData != null)
        {
            AudioClip[] clips = itemData.AttackSound;
            PlaySound(clips);
        }
    }
    private void PlayItemUseSound(InventoryItemData itemData)
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
    private void PlayItemDropSound(InventoryItemData itemData)
    {
        if (itemData != null)
        {
            AudioClip[] clips = itemData.DropSound;
            PlaySound(clips);
        }
    }
    private void PlayPickUpSound(IPickable pickable)
    {
        if (pickable != null)
        {
            AudioClip[] clips = pickable.PickUpSound;
            PlaySound(clips);
        }
    }
    private void PlayInteractSound(IInteractable interactable)
    {
        if (interactable != null)
        {
            
        }
    }
    private void PlayItemPickUpSound(InventoryItemData itemData)
    {
        if (itemData != null)
        {
            AudioClip[] clips = itemData.PickUpSound;
            PlaySound(clips);
        }
    }
    private void InitSoundMap()
    {
        if (playerData is IPlayerSettingProvider settingProvider)
        {
            PlayerSetting playerSetting = settingProvider.PlayerSetting;
            typeToSound.Add(SoundType.BeingDamaged, playerSetting.HealthComponent.DamagedSound);
            typeToSound.Add(SoundType.Steps, playerSetting.RunComponent.StepSounds);
        }
    }
    private void PlaySound(SoundType soundType)
    {
        if (typeToSound.ContainsKey(soundType))
        {
            PlayRandomSound(typeToSound[soundType]);
        }
        else
        {
            Debug.LogError("Sound type not found");
            return;
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
            audioSource.PlayOneShot(clip);
        }
    }
}
