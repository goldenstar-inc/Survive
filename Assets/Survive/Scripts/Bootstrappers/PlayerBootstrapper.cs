using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerBootstrapper : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nicknamePlaceholder;
    
    [Header("Nickname")]
    [SerializeField] private NicknameFollow nicknameFollow;
    [SerializeField] private Transform nicknamePosition;

    [Header("Player Data")]
    [SerializeField] private PlayerDataProvider playerData;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private AmmoHandler ammoHandler;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private SoundController soundController;
    [SerializeField] private PlayerSetting playerSetting;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Components")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator playerWeaponAnimator;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private AudioSource audioSource;

    [Header("Controllers")]
    [SerializeField] private SoundHandler soundHandler;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private InputHandler inputHandler; 
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InventoryController playerInventoryController;
    [SerializeField] private PlayerInteractionDetector playerInteractionDetector;
    [SerializeField] private PlayerPauseController playerPauseController;

    [Header("Camera")]
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform minimapCamera;
    
    [Header("Weapon")]
    [SerializeField] private Transform attackStartPoint;
    [SerializeField] private int inventoryCapacity;

    [Header("Mediators")]
    [SerializeField] private QuestEvents questEvents;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init()
    {
        if (!ValidateSettings()) return;

        InitCamera();
        InitNickname();
        InitSound();
        InitHealth();
        InitAmmo();
        InitMoney();
        InitWeapon();
        InitDialogue();
        InitPlayerData();
        InitSoundHandler();
        InitMovement();
        InitInput();
        InitAnimation();
        InitInventory();
        InitInteraction();
        InitQuestEvents();
        InitQuest();
    }


    /// <summary>
    /// Валидация значений компонентов/сеттингов игрока
    /// </summary>
    /// <returns>True - если все данные валидны, иначе - false</returns>
    private bool ValidateSettings()
    {
        if (nicknamePlaceholder == null || nicknamePosition == null || nicknameFollow == null)
        {
            Debug.LogError("The params on NicknameFollow aren't properly set");
            return false;
        }

        AudioClip[] stepSounds = playerSetting.RunComponent.StepSounds;
        float stepInterval = playerSetting.RunComponent.StepInterval;

        if (stepSounds == null || stepSounds.Length == 0 || stepInterval < 0)
        {
            Debug.LogError("The params of SoundHandler aren't properly set");
            return false;
        }

        float walkSpeed = playerSetting.RunComponent.WalkSpeed;
        float runSpeed = playerSetting.RunComponent.RunSpeed;

        if (walkSpeed <= 0f || runSpeed <= 0f)
        {
            Debug.LogError("Speed values must be greater than zero");
            return false;
        }

        if (playerAnimator == null || weaponManager == null || healthManager == null)
        {
            Debug.LogError("The params of PlayerAnimationController aren't properly set");
            return false;
        }
        
        int maxHealth = playerSetting.HealthComponent.MaxHealth;
        AudioClip damageSound = playerSetting.HealthComponent.DamageSound;
        float invincibilityCooldown =  playerSetting.HealthComponent.InvincibilityCooldown;
        
        if (maxHealth <= 0 || damageSound == null || invincibilityCooldown < 0)
        {
            Debug.LogError("The params of HealthManager aren't properly set");
            return false;
        }

        int maxAmmo = playerSetting.MaxAmmo;
        if (maxAmmo < 0)
        {
            Debug.LogError("The params of AmmoHandler aren't properly set");
            return false;
        }

        if (mainCamera == null || minimapCamera == null)
        {
            Debug.LogError("Cameras aren't properly set");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Инициализация никнейма
    /// </summary>
    private void InitNickname()
    {
        nicknameFollow.Init(
            nicknamePlaceholder,
            nicknamePosition,
            "Melnik"
        );
    }

    /// <summary>
    /// Инициализация камеры игрока
    /// </summary>
    private void InitCamera()
    {
        cameraFollow.Init(
            mainCamera,
            minimapCamera
        );
    }

    /// <summary>
    /// Инициализация звукового контроллера игрока
    /// </summary>
    private void InitSound()
    {
        soundController.Init(
            audioSource
        );
    }

    /// <summary>
    /// Инициализация здоровья игрока
    /// </summary>
    private void InitHealth()
    {
        var health = playerSetting.HealthComponent;

        healthManager.Init(
            health.MaxHealth,
            health.DamageSound,
            health.InvincibilityCooldown,
            soundController
        );
    }

    /// <summary>
    /// Инициализация боезапаса игрока
    /// </summary>
    private void InitAmmo()
    {
        ammoHandler.Init(
            playerSetting.MaxAmmo
        );  
    }

    /// <summary>
    /// Инициализация денежного баланса игрока
    /// </summary>
    private void InitMoney()
    {
        moneyHandler.Init();
    }

    /// <summary>
    /// Инициализация контроллера оружия
    /// </summary>
    private void InitWeapon()
    {
        weaponManager.Init(
            playerWeaponAnimator,
            ammoHandler,
            attackStartPoint
        );
    }

    /// <summary>
    /// Инициализация менеджера диалога
    /// </summary>
    private void InitDialogue()
    {
        dialogueManager.Init(
            playerPauseController
        );  
    }

    /// <summary>
    /// Инициализация фасада данных об игроке
    /// </summary>
    private void InitPlayerData()
    {
        playerData.Init(
            playerSetting,
            healthManager,
            ammoHandler,
            moneyHandler,
            soundController,
            weaponManager,
            questManager,
            dialogueManager
        );
    }

    /// <summary>
    /// [TO FIX]
    /// [TO FIX]
    /// [TO FIX]
    /// Инициализация звукового контроллера для управления звуком ходьбы
    /// [TO FIX]
    /// [TO FIX]
    /// [TO FIX]
    /// </summary>
    private void InitSoundHandler()
    {
        var run = playerSetting.RunComponent;

        soundHandler.Init(
            run.StepSounds,
            run.StepInterval,
            soundController,
            questManager
            );
    }

    /// <summary>
    /// Инициализация компонента движения
    /// </summary>
    private void InitMovement()
    {
        var run = playerSetting.RunComponent;

        playerMovement.Init(
            playerRB,
            run.WalkSpeed,
            run.RunSpeed
            );
    }

    /// <summary>
    /// Инициализация компонентов ввода
    /// </summary>
    private void InitInput()
    {
        inputHandler.Init(
            playerInput,
            playerMovement,
            playerAnimationController,
            soundHandler
            );
    }

    /// <summary>
    /// Инициализация компонента анимации
    /// </summary>
    private void InitAnimation()
    {
        playerAnimationController.Init(
            playerAnimator,
            weaponManager,
            healthManager
            );
    }

    /// <summary>
    /// Инициализация инвентаря
    /// </summary>
    private void InitInventory()
    {
        playerInventoryController.Init(
            inventoryCapacity,
            playerData,
            playerPauseController
        );
    }

    /// <summary>
    /// Инициализация детектора взаимодействий
    /// </summary>
    private void InitInteraction()
    {
        playerInteractionDetector.Init(
            playerData,
            playerInventoryController
            );
    }

    /// <summary>
    /// Инициализация квестовых событий игрока
    /// </summary>
    public void InitQuestEvents()
    {
        questEvents.Init(
            playerInventoryController
            );
    }

    /// <summary>
    /// Инициализация контроллера квестов
    /// </summary>
    private void InitQuest()
    {
        questManager.Init(
            questEvents
        );
    }
}
