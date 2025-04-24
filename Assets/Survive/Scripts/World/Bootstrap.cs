using UnityEngine;
using static ItemConfigsLoader;

public class ItitializeConfigs : MonoBehaviour
{
    public void Init(ItemDatabase configs)
    {
        Initialize(configs.Items);
    }
}