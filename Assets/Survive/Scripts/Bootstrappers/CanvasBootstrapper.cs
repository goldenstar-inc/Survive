using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBootstrapper : MonoBehaviour
{
    [Header("Main canvas")]
    [SerializeField] Canvas canvas;

    [Header("Display systems")]
    [SerializeField] HealthDisplay healthDisplay;
    [SerializeField] AmmoDisplay ammoDisplay;
    [SerializeField] MoneyDisplay moneyDisplay;
    [SerializeField] InventoryDisplay inventoryDisplay;
    [SerializeField] QuestDisplay questDisplay;
    [SerializeField] DialogueDisplay dialogueDisplay;

    [Header("Inventory components")]
    [SerializeField] GameObject[] selectionFrames;
    [SerializeField] Sprite emptySlotImage;
    [SerializeField] Image[] inventoryItemImages;
    [SerializeField] TextMeshProUGUI[] itemQuanityTextFields;

    [Header("Player Bar components")]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI ammoAmountPlaceholder;
    [SerializeField] TextMeshProUGUI balancePlaceholder;

    [Header("Quest components")]
    [SerializeField] TextMeshProUGUI questNamePlaceholder;
    [SerializeField] TextMeshProUGUI questDescriptionPlaceholder;

    [Header("Dialogue components")]
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image portraitImage;
    [SerializeField] Transform choiceContainer;
    [SerializeField] GameObject choiceButtonPrefab;
    [SerializeField] Button closeButton;
    [SerializeField] Button nextLineButton;
    [SerializeField] Sprite interactorPortrait;

    [Header("Minimap")]
    [SerializeField] RawImage minimapImage;

    private HealthManager healthManager;
    private AmmoHandler ammoHandler;
    private MoneyHandler moneyHandler;
    private Camera renderCamera;
    private InventoryController inventoryController;
    private QuestManager questManager;
    private DialogueManager dialogueManager;

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init(
        HealthManager healthManager,
        AmmoHandler ammoHandler,
        MoneyHandler moneyHandler,
        Camera renderCamera,
        InventoryController inventoryController,
        QuestManager questManager,
        DialogueManager dialogueManager
        )
    {
        this.healthManager = healthManager;
        this.ammoHandler = ammoHandler;
        this.moneyHandler = moneyHandler;
        this.renderCamera = renderCamera;
        this.inventoryController = inventoryController;
        this.questManager = questManager;
        this.dialogueManager = dialogueManager;

        if (!Validate()) return;

        InitCamera();
        InitHealthDisplay();
        InitAmmoDisplay();
        InitMoneyDisplay();
        InitInventoryDisplay();
        InitQuestDisplay();
        InitDialogueDisplay();
    }

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
            Debug.LogError("HealthDisplay system isn't loaded correctly");
            return false;
        }

        if (ammoDisplay == null)
        {
            Debug.LogError("AmmoDisplay system isn't loaded correctly");
            return false;
        }

        if (moneyDisplay == null)
        {
            Debug.LogError("MoneyDisplay system isn't loaded correctly");
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

        if (moneyHandler == null)
        {
            Debug.LogError("MoneyHandler not loaded");
            return false;
        }

        if (inventoryDisplay == null)
        {
            Debug.LogError("InventoryDisplay not loaded");
            return false;
        }

        if (questManager == null)
        {
            Debug.LogError("QuestManager not loaded");
            return false;
        }

        if (healthBar == null)
        {
            Debug.LogError("HealthBar not loaded");
            return false;
        }

        if (dialogueDisplay == null)
        {
            Debug.LogError("DialogueController not loaded");
            return false;
        }

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not loaded");
            return false;
        }

        if (closeButton == null)
        {
            Debug.LogError("CloseButton not loaded");
            return false;
        }

        if (nextLineButton == null)
        {
            Debug.LogError("NextLineButton not loaded");
            return false;
        }

        if (minimapImage == null)
        {
            Debug.LogError("MinimapImage not loaded");
            return false;
        }

        return true;
    }

    private void InitCamera()
    {
        canvas.worldCamera = renderCamera;
    }

    private void InitHealthDisplay()
    {
        healthDisplay.Init(
            healthManager,
            healthBar
        );
    }

    private void InitAmmoDisplay()
    {
        ammoDisplay.Init(
            ammoHandler,
            ammoAmountPlaceholder
        );
    }

    private void InitMoneyDisplay()
    {
        moneyDisplay.Init(
            moneyHandler,
            balancePlaceholder
        );
    }

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

    private void InitQuestDisplay()
    {
        questDisplay.Init(
            questNamePlaceholder,
            questDescriptionPlaceholder,
            questManager
        );
    }

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
            dialogueManager,
            questManager,
            interactorPortrait
        );
    }
}
