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
    /// Интервал между звуками
    /// </summary>
    private float swingInterval = 0.1f;

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
            if (Time.time - lastSwingTime > swingInterval)
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
                DamageHandler damageHandler = collision.GetComponent<DamageHandler>();
                if (damageHandler != null)
                {
                    damageHandler.TakeDamage(Knife.damage);
                }
            }
        }
    }
}