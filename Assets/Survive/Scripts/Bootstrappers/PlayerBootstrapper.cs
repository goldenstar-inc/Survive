using System;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private AmmoHandler ammoHandler;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private SoundController soundController;
    [SerializeField] private PlayerSetting playerSetting;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Components")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private AudioSource audioSource;

    [Header("Controllers")]
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private InputHandler inputHandler; 
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerInteractionDetector interactionDetector;
    [SerializeField] private PlayerPauseController pauseController;

    [Header("Camera")]
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform minimapCamera;
    
    [Header("Weapon")]
    [SerializeField] private Transform attackStartPoint;
    [SerializeField] private int inventoryCapacity;

    [Header("Mediators")]
    [SerializeField] private QuestEvents questEvents;

    [Header("SoundPacks")]
    [SerializeField] private UISoundPack UISoundPack;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init()
    {
        if (!ValidateSettings()) return;

        InitCamera();
        InitNickname();
        InitHealth();
        InitAmmo();
        InitMoney();
        InitWeapon();
        InitDialogue();
        InitPlayerData();
        InitSound();
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

        if (playerAnimator == null || weaponManager == null || healthHandler == null)
        {
            Debug.LogError("The params of PlayerAnimationController aren't properly set");
            return false;
        }
        
        int maxHealth = playerSetting.HealthComponent.MaxHealth;
        AudioClip[] damageSound = playerSetting.HealthComponent.DamagedSound;
        float invincibilityCooldown =  playerSetting.HealthComponent.InvincibilityCooldown;
        
        if (maxHealth <= 0 || damageSound == null  || damageSound.Length == 0 ||invincibilityCooldown < 0)
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
    /// Инициализация здоровья игрока
    /// </summary>
    private void InitHealth()
    {
        var health = playerSetting.HealthComponent;

        healthHandler.Init(
            health,
            health.MaxHealth,
            health.InvincibilityCooldown
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
            weaponAnimator,
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
            pauseController
        );  
    }

    /// <summary>
    /// Инициализация фасада данных об игроке
    /// </summary>
    private void InitPlayerData()
    {
        playerData.Init(
            playerSetting,
            healthHandler,
            ammoHandler,
            moneyHandler,
            weaponManager,
            questManager,
            dialogueManager
        );
    }

    /// <summary>
    /// Инициализация звукового контроллера игрока
    /// </summary>
    private void InitSound()
    {
        soundController.Init(
            audioSource,
            playerData,
            healthHandler,
            inventoryController,
            questManager,
            playerMovement,
            weaponManager,
            interactionDetector
        );
    }

    /// <summary>
    /// Инициализация компонента движения
    /// </summary>
    private void InitMovement()
    {
        var run = playerSetting.RunComponent;

        playerMovement.Init(
            run,
            playerRB,
            run.WalkSpeed,
            run.RunSpeed,
            run.StepInterval
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
            playerAnimationController
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
            healthHandler
            );
    }

    /// <summary>
    /// Инициализация инвентаря
    /// </summary>
    private void InitInventory()
    {
        inventoryController.Init(
            inventoryCapacity,
            playerData,
            pauseController,
            UISoundPack
        );
    }

    /// <summary>
    /// Инициализация детектора взаимодействий
    /// </summary>
    private void InitInteraction()
    {
        interactionDetector.Init(
            playerData,
            inventoryController
            );
    }

    /// <summary>
    /// Инициализация квестовых событий игрока
    /// </summary>
    public void InitQuestEvents()
    {
        questEvents.Init(
            inventoryController,
            weaponManager
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
