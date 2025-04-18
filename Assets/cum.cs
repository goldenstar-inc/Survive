using UnityEngine;

public class cum : MonoBehaviour
{
    public Shader shader;
    public Camera miniMapCamera;

    void Start()
    {
        miniMapCamera.RenderWithShader(shader, "");
    }
}
