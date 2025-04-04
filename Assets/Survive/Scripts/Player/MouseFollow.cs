using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за вращение объекта в сторону курсора мыши
/// </summary>
public class MouseFollow : MonoBehaviour
{
    /// <summary>
    /// Центральная точка, вокруг которой вращается объект (например, игрок)
    /// </summary>
    public Transform centerObject;

    /// <summary>
    /// Объект, который будет вращаться (например, оружие)
    /// </summary>
    public Transform objectToRotate;

    /// <summary>
    /// Расстояние от центральной точки до объекта
    /// </summary>
    public float radius;

    /// <summary>
    /// Метод для получения направления к курсору мыши
    /// </summary>
    /// <returns>Направление от центральной точки к курсору</returns>
    private Vector3 GetDirectionToMouse()
    {
        // Получаем позицию курсора в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Убираем Z-координату

        // Вычисляем направление от центральной точки к курсору
        Vector3 direction = (mousePosition - centerObject.position).normalized;
        return direction;
    }

    /// <summary>
    /// Метод для вращения объекта в сторону курсора
    /// </summary>
    public void RotateObjectTowardsMouse()
    {
        // Получаем направление к курсору
        Vector3 direction = GetDirectionToMouse();

        // Вычисляем угол между направлением и осью X
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Вычисляем новую позицию объекта на расстоянии radius от центра
        float x = centerObject.position.x + radius * math.cos(angle);
        float y = centerObject.position.y + radius * math.sin(angle);

        // Преобразуем угол из радиан в градусы
        angle *= Mathf.Rad2Deg;

        // Обновляем позицию и поворот объекта
        if (objectToRotate != null)
        {
            objectToRotate.position = new Vector3(x, y, 0);
            objectToRotate.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    /// <summary>
    /// Метод Update вызывается каждый кадр
    /// </summary>
    private void Update()
    {
        RotateObjectTowardsMouse();
    }
}