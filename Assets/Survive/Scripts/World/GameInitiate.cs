using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class GameInitiate : MonoBehaviour
{

    [SerializeField] EventSystem eventSystem;
    [SerializeField] Light2D globalLight;
    [SerializeField] GameObject player;
    [SerializeField] GameObject world;
    [SerializeField] Canvas canvas;

    private async void Start()
    {
        BindObjects();
    }


    private void BindObjects()
    {
        world = Instantiate(world);
        player = Instantiate(player);
        //canvas = Instantiate(canvas);
        eventSystem = Instantiate(eventSystem);
        globalLight = Instantiate(globalLight);
    }
}