using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Cam : MonoBehaviour
{
    void Start()
    {
        ApplySettings();
    }

    void ApplySettings()
    {
        var cam = GetComponent<Camera>();
        var additionalCamData = cam.GetUniversalAdditionalCameraData();

        if (additionalCamData != null)
        {
            additionalCamData.renderShadows = false;
            additionalCamData.requiresDepthTexture = false;
            additionalCamData.requiresColorTexture = false;
            additionalCamData.renderPostProcessing = false;

            Debug.Log("✅ Minimap Camera settings applied.");
        }
        else
        {
            Debug.LogWarning("⚠️ Could not find Universal Additional Camera Data!");
        }
    }
}
