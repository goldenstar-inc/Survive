using UnityEngine;
using static ItemConfigsLoader;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] ItemDatabase configs;

    private void Start()
    {
        Initialize(configs.Items);
    }
}