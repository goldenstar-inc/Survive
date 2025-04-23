using UnityEngine;
using static ItemConfigsLoader;

public class ItitializeConfigs : MonoBehaviour
{
    [SerializeField] ItemDatabase configs;

    private void Init()
    {
        Initialize(configs.Items);
    }
}