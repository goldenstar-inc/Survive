using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] InitializeBehaviour[] playerScripts;

    private void Start()
    {
        foreach (InitializeBehaviour script in playerScripts)
        {
            Debug.Log(script);
            script.Initialize();
        }
    }
}
