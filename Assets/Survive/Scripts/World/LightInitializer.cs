using UnityEngine;

public class LightInitializer : MonoBehaviour
{
    [SerializeField] InitializeBehaviour[] lightScripts;

    void Start()
    {
        foreach (InitializeBehaviour script in lightScripts)
        {
            script.Initialize();
        }
    }
}
