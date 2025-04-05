using UnityEngine;

public class WorldInitializer : MonoBehaviour
{
    [SerializeField] InitializeBehaviour[] worldScripts;

    void Start()
    {
        foreach (InitializeBehaviour script in worldScripts)
        {
            script.Initialize();
        }
    }
}
