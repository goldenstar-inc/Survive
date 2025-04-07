using UnityEngine;
using Zenject;

/// <summary>
/// Класс, отвечающий за следование камеры за игроком
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    public void Initialize(ICameraProvider cameraProvider)
    {
        cameraTransform = cameraProvider.GetCameraTransform();
    }
    
    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (cameraTransform != null)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
        
            cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
