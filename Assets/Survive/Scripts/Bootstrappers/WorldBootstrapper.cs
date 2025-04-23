using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldBootstrapper : MonoBehaviour
{
    [Header("World")]
    [SerializeField] GameObject world;

    [Header("World components")]
    [SerializeField] Light2D worldLight;
    [SerializeField] AudioSource worldAudioSource;
    [SerializeField] GameObject worldEvents;    

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Player UI")]
    [SerializeField] GameObject playerCanvas;

    [Header("NPCs")]
    [SerializeField] GameObject[] NPCs;

    [Header("BuyZones")]
    [SerializeField] GameObject[] buyZones;

    void Start()
    {
        if (!ValidatePrefabs()) return;

        CreateNewWorld();
        SpawnWorldLight();
        SpawnWorldAudioSource();
        SpawnWorldEvents();
        SpawnNewPlayer();
    }
    private bool ValidatePrefabs()
    {
        if (world == null)
        {
            Debug.LogError("World isn't loaded");
            return false;
        }

        if (worldLight == null)
        {
            Debug.LogError("WorldLight isn't loaded");
            return false;
        }

        if (worldAudioSource == null)
        {
            Debug.LogError("WorldAudioSource isn't loaded");
            return false;
        }

        if (worldEvents == null)
        {
            Debug.LogError("WorldEvents isn't loaded");
            return false;
        }

        if (player == null)
        {
            Debug.LogError("Player isn't loaded");
            return false;
        }

        if (playerCanvas == null)
        {
            Debug.LogError("PlayerCanvas isn't loaded");
            return false;
        }

        if (NPCs == null)
        {
            Debug.LogError("NPCs isn't loaded");
            return false;
        }

        if (buyZones == null)
        {
            Debug.LogError("BuyZones isn't loaded");
            return false;
        }

        return true;
    }

    private void SpawnNewPlayer()
    {
        GameObject spawnedPlayer = Instantiate(player);
        PlayerBootstrapper playerBootstrapper = spawnedPlayer.GetComponentInChildren<PlayerBootstrapper>();
        playerBootstrapper.Init();
        Camera camera = spawnedPlayer.GetComponentInChildren<Camera>();
        HealthManager healthManager = spawnedPlayer.GetComponentInChildren<HealthManager>();
        AmmoHandler ammoHandler = spawnedPlayer.GetComponentInChildren<AmmoHandler>();
        MoneyHandler moneyHandler = spawnedPlayer.GetComponentInChildren<MoneyHandler>();
        InventoryController inventoryController = spawnedPlayer.GetComponentInChildren<InventoryController>();
        QuestManager questManager = spawnedPlayer.GetComponentInChildren<QuestManager>();
        DialogueManager dialogueManager = spawnedPlayer.GetComponentInChildren<DialogueManager>();
        
        GameObject spawnedCanvas = Instantiate(playerCanvas);
        CanvasBootstrapper canvasBootstrapper = spawnedCanvas.GetComponentInChildren<CanvasBootstrapper>();
        canvasBootstrapper.Init(healthManager, ammoHandler, moneyHandler, camera, inventoryController, questManager, dialogueManager);
    }

    private void CreateNewWorld()
    {
        Instantiate(world);
    }

    private void SpawnWorldLight()
    {
        Instantiate(worldLight);
    }

    private void SpawnWorldAudioSource()
    {
        Instantiate(worldAudioSource);
    }

    private void SpawnWorldEvents()
    {
        Instantiate(worldEvents);
    }
}
