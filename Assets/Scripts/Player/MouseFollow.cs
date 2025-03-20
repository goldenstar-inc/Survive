using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за вращение объекта в сторону курсора
/// </summary>
public class MouseFollow : MonoBehaviour
{
    /// <summary>
    /// Компонент Transform вращаемого объекта
    /// </summary>
    public Transform transformObject;

    /// <summary>
    /// Расстояние от игрока на котором вращается объект
    /// </summary>
    public float radius;

    /// <summary>
    /// Метод получающий направление от игрока к курсору
    /// </summary>
    /// <returns>Направление от игрока к курсору в виде вектора</returns>
    private Vector3 GetDirectionToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transformObject.position).normalized;
        return direction;
    }

    /// <summary>
    /// Метод, поворачивающий объект в сторону курсора
    /// </summary>
    public void RotateObjectTowardsMouse()
    {
        Vector3 direction = GetDirectionToMouse();

        float angle = Mathf.Atan2(direction.y, direction.x);

        float x = transformObject.position.x + radius * math.cos(angle);
        float y = transformObject.position.y + radius * math.sin(angle);

        angle *= Mathf.Rad2Deg;

        transform.position = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    public void Update()
    {
        RotateObjectTowardsMouse();
    }
}