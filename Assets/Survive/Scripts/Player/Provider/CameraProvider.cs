using UnityEngine;
public class CameraProvider : ICameraProvider
{
    private Transform cameraTransform;

    public CameraProvider(Transform cameraTransform)
    {
        this.cameraTransform = cameraTransform;
    }

    public Transform GetCameraTransform()
    {
        return cameraTransform;
    }
}
