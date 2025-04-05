using UnityEngine;

public class CanvasInitializer : MonoBehaviour
{
    [SerializeField] InitializeBehaviour[] canvasScripts;

    void Start()
    {
        foreach (InitializeBehaviour script in canvasScripts)
        {
            script.Initialize();
        }
    }
}