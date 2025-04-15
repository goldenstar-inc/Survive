using UnityEngine;

public class WorldBootstrapper : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCanvas;
    void Start()
    {
        if (!ValidatePrefabs()) return;

        SpawnNewPlayer();
    }
    private bool ValidatePrefabs()
    {
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
        InventoryController inventoryController = spawnedPlayer.GetComponentInChildren<InventoryController>();
        QuestManager questManager = spawnedPlayer.GetComponentInChildren<QuestManager>();
        
        GameObject spawnedCanvas = Instantiate(playerCanvas);
        CanvasBootstrapper canvasBootstrapper = spawnedCanvas.GetComponentInChildren<CanvasBootstrapper>();
        canvasBootstrapper.Init(healthManager, ammoHandler, camera, inventoryController, questManager);
    }
}
