using UnityEngine;

/// <summary>
/// �����, ����������� �������� ����
/// </summary>
public class ShotgunBullet : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ���������
    /// </summary>
    public int damage => Shotgun.damage;

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
                DamageHandler damageHandler = collision.GetComponent<DamageHandler>();

                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(damage);
                }
            }
        }
    }
}
