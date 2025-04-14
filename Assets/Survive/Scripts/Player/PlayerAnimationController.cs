using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Класс, отвечающий за воспроизведение анимаций игрока
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    private HealthManager healthManager;

    private WeaponManager weaponManager;

    /// <summary>
    /// Аниматор, отвечающий за анимирование игрока
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Текущая позиция курсора
    /// </summary>
    private Vector3 currentMousePosition;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="weaponManager"></param>
    /// <param name="healthManager"></param>
    public void Init(Animator animator, WeaponManager weaponManager, HealthManager healthManager)
    {
        this.animator = animator;
        this.weaponManager = weaponManager;
        this.healthManager = healthManager;

        healthManager.OnTakeDamage += OnDamageTaken;
        weaponManager.OnAttack += EnableAttackingState;
    }

    /// <summary>
    /// Метод, обновляющий анимацию игрока
    /// </summary>
    public void UpdateMovementAnimation(Vector3 input)
    {
        if (input != null && currentMousePosition != null && animator != null && animator.GetBool("IsDamaged") == false)
        {
            if (input.x != 0 || input.y != 0)
            {
                currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                animator.SetBool("IsIdle", false);
                float angle = Mathf.Atan2(currentMousePosition.y, currentMousePosition.x) * Mathf.Rad2Deg;

                if (angle >= -45 && angle < 45)
                {
                    if (animator.GetBool("IsAttacking"))
                    {
                        animator.Play("ArmlessWalkingRight");
                    }   
                    else
                    {
                        animator.Play("WalkingRight");
                    }
                }
                else if (angle >= 45 && angle < 135)
                {
                    if (animator.GetBool("IsAttacking"))
                    {
                        animator.Play("ArmlessWalkingUp");
                    }   
                    else
                    {
                        animator.Play("WalkingUp");
                    }
                }
                else if ((angle >= 135 && angle <= 180) || (angle >= -180 && angle < -135))
                {
                    if (animator.GetBool("IsAttacking"))
                    {
                        animator.Play("ArmlessWalkingLeft");
                    }
                    else
                    {
                        animator.Play("WalkingLeft");
                    }
                }
                else if (angle >= -135 && angle < -45)
                {
                    if (animator.GetBool("IsAttacking"))
                    {
                        animator.Play("ArmlessWalkingDown");
                    }   
                    else
                    {
                        animator.Play("WalkingDown");
                    }
                }
            }
            else
            {
                animator.SetBool("IsIdle", true);
            }
        }
    }

    /// <summary>
    /// Метод, вызывающийся при получении урона
    /// </summary>
    /// <param name="currentHealth">Текущее количество очков здоровья</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    public void OnDamageTaken(int currentHealth, int maxHealth)
    {
        if (animator != null)
        {
            animator.SetBool("IsDamaged", true);
        }
    }

    /// <summary>
    /// Установка значения false для параметра IsDamaged
    /// </summary>
    public void ResetDamageState()
    {
        if (animator != null)
        {
            animator.SetBool("IsDamaged", false);
        }
    }

    public void EnableAttackingState()
    {
        if (animator != null)
        {
            animator.SetBool("IsAttacking", true);
        }
    }

    public void DisableAttackingState()
    {
        if (animator != null)
        {
            animator.SetBool("IsAttacking", false);
        }
    }
    void OnDisable()
    {
        if (healthManager != null)
        {
            healthManager.OnTakeDamage -= OnDamageTaken;
        }

        if (weaponManager != null)
        {
            weaponManager.OnAttack -= EnableAttackingState;
        }
    }
}