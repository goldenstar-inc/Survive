using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// �����, ���������� �� �������� ������� � ������� �������
/// </summary>
public class MouseFollow : MonoBehaviour
{
    /// <summary>
    /// ��������� Transform ���������� �������
    /// </summary>
    public Transform transformObject;

    /// <summary>
    /// ���������� �� ������ �� ������� ��������� ������
    /// </summary>
    public float radius;

    /// <summary>
    /// ����� ���������� ����������� �� ������ � �������
    /// </summary>
    /// <returns>����������� �� ������ � ������� � ���� �������</returns>
    private Vector3 GetDirectionToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transformObject.position).normalized;
        return direction;
    }

    /// <summary>
    /// �����, �������������� ������ � ������� �������
    /// </summary>
    public void RotateObjectTowardsMouse()
    {
        Vector3 direction = GetDirectionToMouse();

        float angle = Mathf.Atan2(direction.y, direction.x);

        float x = transformObject.position.x + radius * math.cos(angle);
        float y = transformObject.position.y + radius * math.sin(angle);

        angle *= Mathf.Rad2Deg;

        transform.position = new Vector3(x, y, 0);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// �����, ������������ ������ ������� ����
    /// </summary>
    public void Update()
    {
        RotateObjectTowardsMouse();
    }
}