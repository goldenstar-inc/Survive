using System.Linq;
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

    [Header("Configs")]
    [SerializeField] ItitializeConfigs configLoader; 
    [SerializeField] ItemDatabase database;   

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Player UI")]
    [SerializeField] GameObject playerCanvas;

    [Header("NPCs")]
    [SerializeField] GameObject[] NPCs;
    [SerializeField] Vector3[] spawnPoints;

    [Header("BuyZones")]
    [SerializeField] GameObject[] buyZones;

    void Start()
    {
        if (!ValidatePrefabs()) return;

        configLoader.Init(database);

        CreateNewWorld();
        SpawnWorldLight();
        SpawnWorldAudioSource();
        SpawnWorldEvents();
        SpawnNewPlayer();
        
        for (int i = 0; i < NPCs.Length; i++)
        {
            Spawn(NPCs[i], spawnPoints[i], Quaternion.identity);
        }
    }
    private bool ValidatePrefabs()
    {
        if (configLoader == null || database == null)
        {
            Debug.LogError("Configs not loaded");
            return false;
        }
        
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
        HealthHandler healthManager = spawnedPlayer.GetComponentInChildren<HealthHandler>();
        AmmoHandler ammoHandler = spawnedPlayer.GetComponentInChildren<AmmoHandler>();
        MoneyHandler moneyHandler = spawnedPlayer.GetComponentInChildren<MoneyHandler>();
        InventoryController inventoryController = spawnedPlayer.GetComponentInChildren<InventoryController>();
        QuestManager questManager = spawnedPlayer.GetComponentInChildren<QuestManager>();
        DialogueManager dialogueManager = spawnedPlayer.GetComponentInChildren<DialogueManager>();
        Inventory inventory = spawnedPlayer.GetComponentInChildren<Inventory>();

        GameObject spawnedCanvas = Instantiate(playerCanvas);
        CanvasBootstrapper canvasBootstrapper = spawnedCanvas.GetComponentInChildren<CanvasBootstrapper>();
        canvasBootstrapper.Init(
            healthManager, 
            ammoHandler, 
            moneyHandler, 
            camera, 
            inventoryController, 
            questManager, 
            dialogueManager, 
            inventory
            );
    }

    private void CreateNewWorld()
    {
        Spawn(world, Vector2.zero, Quaternion.identity);
    }

    private void SpawnWorldLight()
    {
        Spawn(worldLight, Vector2.zero, Quaternion.identity);
    }

    private void SpawnWorldAudioSource()
    {
        Spawn(worldAudioSource, Vector2.zero, Quaternion.identity);
    }

    private void SpawnWorldEvents()
    {
        Spawn(worldEvents, Vector2.zero, Quaternion.identity);
    }

    private void Spawn(Object objectToSpawn, Vector3 position, Quaternion rotation)
    {
        Instantiate(objectToSpawn, position, rotation);
    }
}
