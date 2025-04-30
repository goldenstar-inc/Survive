using UnityEngine;

/// <summary>
/// Класс, отвечающий за следование камеры за игроком
/// </summary>
public class CameraFollow : MonoBehaviour
{
    private Transform mainCamera;
    private Transform minimapCamera;
    private float smoothTime = 0.3f;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float freezedZ = -10f;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="mainCamera">Главная камера</param>
    /// <param name="minimapCamera">Камера мини-карты</param>
    public void Init(
        Transform mainCamera,
        Transform minimapCamera
        )
    {
        this.mainCamera = mainCamera;
        this.minimapCamera = minimapCamera;
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        CalculateCurrentTargetPosition();
        MoveMainViewCamera();
        MoveMinimapCamera();
    }

    /// <summary>
    /// Передвижение главной камеры
    /// </summary>
    private void MoveMainViewCamera()
    {
        if (mainCamera != null)
        {
            mainCamera.position = Vector3.SmoothDamp(mainCamera.position, targetPosition, ref velocity, smoothTime);
        }
    }

    /// <summary>
    /// Передвижение камеры мини-карты
    /// </summary>
    private void MoveMinimapCamera()
    {
        if (minimapCamera != null)
        {
            minimapCamera.transform.position = targetPosition;
        }
    }

    /// <summary>
    /// Вычисление позиции цели
    /// </summary>
    private void CalculateCurrentTargetPosition()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y, freezedZ);
    }
}
