using UnityEngine;

/// <summary>
/// �����, ����������� �������� ����
/// </summary>
public class ShotgunBullet : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ���������
    /// </summary>
    public int damage => 20;

    /// <summary>
    /// �����, ������������� ��� ����� � ������� 
    /// </summary>
    /// <param name="collision">������ ��������</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                HealthManager damageHandler = collision.GetComponent<HealthManager>();

                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(damage);
                }
            }
        }
    }
}
