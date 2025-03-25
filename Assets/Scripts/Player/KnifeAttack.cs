using System.Collections;
using UnityEngine;
using static InventoryController;

/// <summary>
/// �����, ���������� �� ����� �������� �������
/// </summary>
public class KnifeAttack : MonoBehaviour, IAttackScript
{
    /// <summary>
    /// ������, ���������� ��������
    /// </summary>
    public GameObject attackingRange;

    /// <summary>
    /// ��������, ���������� �� �������� �����
    /// </summary>
    public Animator attackingAnimator;

    /// <summary>
    /// Время последнего проигрывания звука
    /// </summary>
    private float lastSwingTime = 0f;

    /// <summary>
    /// Урон с ножа
    /// </summary>
    public int damage = Knife.damage;

    /// <summary>
    /// Интервал между звуками
    /// </summary>
    public float attackSpeed => Knife.attackSpeed;

    /// <summary>
    /// Коллайдер атаки
    /// </summary>
    public CircleCollider2D attackingCollider2D;

    /// <summary>
    /// Метод атаки ножом
    /// </summary>
    public void Attack()
    {
        EnableAnimation();
        PlaySwingingKnifeSound();
    }
    
    /// <summary>
    /// Проигрывает звук размахивания холодным оружием
    /// </summary>
    private void PlaySwingingKnifeSound()
    {
        if (!SoundController.Instance.weaponAudioSource.isPlaying)
        {
            if (Time.time - lastSwingTime > attackSpeed)
            {
                SoundController.Instance.PlayRandomSwingingKnifeSound();
                lastSwingTime = Time.time;
            }
        }
    }

    /// <summary>
    /// Функция, включающая анимацию атаки
    /// </summary>
    public void EnableAnimation() 
    {
        attackingCollider2D.enabled = true;
        attackingAnimator.SetBool("IsAttacking", true);
    }

    /// <summary>
    /// Функция, выключающая анимацию атаки
    /// </summary>
    public void DisableAnimation() 
    {
        attackingCollider2D.enabled = false;
        attackingAnimator.SetBool("IsAttacking", false);
    }

    /// <summary>
    /// Функция, применяемая при контакте с триггером
    /// </summary>
    /// <param name="collision">Коллизия триггера</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(damage);
                DamageHandler damageHandler = collision.GetComponent<DamageHandler>();
                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(damage);
                }
            }
        }
    }
}