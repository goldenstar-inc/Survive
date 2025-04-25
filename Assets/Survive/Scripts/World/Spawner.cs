using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 coords, Quaternion rotation)
    {
        return Instantiate(prefab, coords, rotation);
    }
}