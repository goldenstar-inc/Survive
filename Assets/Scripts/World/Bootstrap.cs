using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] public ItemDatabase configs;
    private void Start()
    {
        if (configs == null)
        {
            Debug.LogError("Item configs not loaded");
        }
        else
        {
            ItemConfigsLoader.Initialize(configs.Items);
        }

    }
}
