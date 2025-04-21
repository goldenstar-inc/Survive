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

    [Header("Minimap")]
    [SerializeField] RawImage minimapImage;

    // 👇 ДОБАВЛЕНО: QuestGiver
    private QuestGiver questGiver;

    private HealthManager healthManager;
    private AmmoHandler ammoHandler;
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
        Camera renderCamera,
        InventoryController inventoryController,
        QuestManager questManager,
        DialogueManager dialogueManager,
        QuestGiver questGiver // ДОБАВЛЕНО
        )
    {
        this.healthManager = healthManager;
        this.ammoHandler = ammoHandler;
        this.renderCamera = renderCamera;
        this.inventoryController = inventoryController;
        this.questManager = questManager;
        this.dialogueManager = dialogueManager;
        this.questGiver = questGiver; // ДОБАВЛЕНО

        if (!Validate()) return;

        InitCamera();
        InitHealthDisplay();
        InitAmmoDisplay();
        InitInventoryDisplay();
        InitQuestDisplay();
        InitDialogueDisplay(); // QuestGiver будет передан сюда
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
            questManager    //  И QuestManager
        );
    }
}
