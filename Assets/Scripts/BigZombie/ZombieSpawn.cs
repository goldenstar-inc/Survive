using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieSpawn : MonoBehaviour
{
    /// <summary>
    /// Префаб объекта для появления
    /// </summary>
    public GameObject prefabToSpawn;

    /// <summary>
    /// Ссылка на Tilemap
    /// </summary>
    public Tilemap tilemap;

    /// <summary>
    /// Количество зомби которых нужно заспавнить
    /// </summary>
    public float ZombiesToSpawn;

    void Start()
    {
        RandomSpawn();
    }

    private void RandomSpawn()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int i = 0; i < ZombiesToSpawn; i++)
        {
            while (true)
            {
                Vector3Int randomCell = new Vector3Int(
                    Random.Range(bounds.xMin, bounds.xMax),
                    Random.Range(bounds.yMin, bounds.yMax),
                    0
                );

                if (!tilemap.HasTile(randomCell))
                {
                    Vector3 spawnPosition = tilemap.CellToWorld(randomCell) + new Vector3(0.5f, 0.5f, 0);
                    Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                    break;
                }
            }
        }
    }

}
