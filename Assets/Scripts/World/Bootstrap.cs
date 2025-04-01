using UnityEngine;
using static ItemConfigsLoader;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] ItemDatabase configs;
    [SerializeField] InitializableBehaviour[] initializables;

    private void Start()
    {
        ItemConfigsLoader.Initialize(configs.Items);

        foreach (InitializableBehaviour initializable in initializables)
        {
            initializable.Initialize();
        }
    }
}