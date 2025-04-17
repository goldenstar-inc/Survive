using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBootstrapper : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] HealthDisplay healthDisplay;
    [SerializeField] AmmoDisplay ammoDisplay;
    [SerializeField] InventoryDisplay inventoryDisplay;
    [SerializeField] QuestDisplay questDisplay;
    [SerializeField] Slider healthBar;
    [SerializeField] DialogueDisplay dialogueDisplay;
    private HealthManager healthManager;
    private AmmoHandler ammoHandler;
    private Camera renderCamera;
    private InventoryController inventoryController;
    private QuestManager questManager;
    private DialogueManager dialogueManager;
    [SerializeField] GameObject[] selectionFrames;
    [SerializeField] Sprite emptySlotImage;
    [SerializeField] Image[] inventoryItemImages;
    [SerializeField] TextMeshProUGUI[] itemQuanityTextFields;

    [SerializeField] TextMeshProUGUI questNamePlaceholder;
    [SerializeField] TextMeshProUGUI questDescriptionPlaceholder;
    [SerializeField] TextMeshProUGUI ammoAmountPlaceholder;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image portraitImage;
    [SerializeField] Transform choiceContainer;
    [SerializeField] GameObject choiceButtonPrefab;
    [SerializeField] Button closeButton;
    [SerializeField] Button nextLineButton;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="healthManager">Скрипт, отвечающий за управление здоровьем</param>
    /// <param name="ammoHandler">Скрипт, управляющий боезапасом</param>
    public void Init(
        HealthManager healthManager, 
        AmmoHandler ammoHandler,
        Camera renderCamera,
        InventoryController inventoryController,
        QuestManager questManager,
        DialogueManager dialogueManager
        )
    {
        this.healthManager = healthManager;
        this.ammoHandler = ammoHandler;
        this.renderCamera = renderCamera;
        this.inventoryController = inventoryController;
        this.questManager = questManager;
        this.dialogueManager = dialogueManager;

        if (!Validate()) return;

        InitCamera();
        InitHealthDisplay();
        InitAmmoDisplay();
        InitInventoryDisplay();
        InitQuestDisplay();
        InitDialogueDisplay();
        InitDialogueManager();
    }

    /// <summary>
    /// Валидация скриптов и систем канваса
    /// </summary>
    /// <returns>True - если все скрипты и системы пргружены корректно, иначе - false</returns>
    private bool Validate()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas not loaded");
            return false;
        }

        if (renderCamera == null)
        {
            Debug.LogError("Camera not loaded");
            return false;
        }

        if (healthDisplay == null)
        {
            Debug.LogError("Health display system isn't loaded correctly");
            return false;
        }

        if (ammoDisplay == null)
        {
            Debug.LogError("Ammo display system isn't loaded correctly");
            return false;
        }

        if (healthManager == null)
        {
            Debug.LogError("HealthManager not loaded");
            return false;
        }

        if (ammoHandler == null)
        {
            Debug.LogError("AmmoHandler not loaded");
            return false;
        }

        if (inventoryDisplay == null)
        {
            Debug.LogError("InventoryDisplay not loaded");
        }

        if (questManager == null)
        {
            Debug.LogError("QuestManager not loaded");
        }
         
        if (healthBar == null)
        {
            Debug.LogError("HealthBar not loaded");
        }

        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueController not loaded");
        }

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueController not loaded");
        }

        if (closeButton == null)
        {
            Debug.LogError("CloseButton not loaded");
        }

        if (nextLineButton == null)
        {
            Debug.LogError("NextLineButton not loaded");
        }

        return true;
    }

    /// <summary>
    /// Инициализация камеры
    /// </summary>
    private void InitCamera()
    {
        canvas.worldCamera = renderCamera;
    }

    /// <summary>
    /// Инициализация системы отображения здоровья
    /// </summary>
    private void InitHealthDisplay()
    {
        healthDisplay.Init(
            healthManager, 
            healthBar
            );
    }

    /// <summary>
    /// Инициализация системы отображения боезапаса
    /// </summary>
    private void InitAmmoDisplay()
    {
        ammoDisplay.Init(
            ammoHandler,
            ammoAmountPlaceholder
            );
    }

    /// <summary>
    /// Инициализация системы отображения боезапаса
    /// </summary>
    private void InitInventoryDisplay()
    {
        inventoryDisplay.Init(
            selectionFrames,
            emptySlotImage,
            inventoryItemImages,
            itemQuanityTextFields,
            inventoryController
            );
    }

    /// <summary>
    /// Инициализация системы отображения активного квеста
    /// </summary>
    private void InitQuestDisplay()
    {
        questDisplay.Init(
            questNamePlaceholder,
            questDescriptionPlaceholder,
            questManager
        );
    }

    /// <summary>
    /// Инициализация системы отображения диалогов
    /// </summary>
    private void InitDialogueDisplay()
    {
        dialogueDisplay.Init(
            closeButton,
            nextLineButton,
            dialogueText,
            dialoguePanel,
            nameText,
            portraitImage,
            choiceContainer,
            choiceButtonPrefab,
            dialogueManager
        );
    }

    private void InitDialogueManager()
    {
        dialogueManager.Init();
    }
}