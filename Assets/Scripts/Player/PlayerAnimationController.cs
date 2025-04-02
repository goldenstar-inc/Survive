using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Класс, отвечающий за воспроизведение анимаций игрока
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] HealthManager playerHealthManager;

    /// <summary>
    /// Аниматор, отвечающий за анимирование игрока
    /// </summary>
    private Animator playerAnimator;

    /// <summary>
    /// Текущая позиция курсора
    /// </summary>
    private Vector3 currentMousePosition;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerHealthManager.OnTakeDamage += OnDamageTaken;
        WeaponManager.Instance.OnAttack += EnableAttackingState;
    }

    /// <summary>
    /// Метод, обновляющий анимацию игрока
    /// </summary>
    public void UpdateMovementAnimation(Vector3 input)
    {
        if (input != null && currentMousePosition != null && playerAnimator != null && playerAnimator.GetBool("IsDamaged") == false)
        {
            if (input.x != 0 || input.y != 0)
            {
                currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                playerAnimator.SetBool("IsIdle", false);
                float angle = Mathf.Atan2(currentMousePosition.y, currentMousePosition.x) * Mathf.Rad2Deg;

                if (angle >= -45 && angle < 45)
                {
                    if (playerAnimator.GetBool("IsAttacking"))
                    {
                        playerAnimator.Play("ArmlessWalkingRight");
                    }   
                    else
                    {
                        playerAnimator.Play("WalkingRight");
                    }
                }
                else if (angle >= 45 && angle < 135)
                {
                    if (playerAnimator.GetBool("IsAttacking"))
                    {
                        playerAnimator.Play("ArmlessWalkingUp");
                    }   
                    else
                    {
                        playerAnimator.Play("WalkingUp");
                    }
                }
                else if ((angle >= 135 && angle <= 180) || (angle >= -180 && angle < -135))
                {
                    if (playerAnimator.GetBool("IsAttacking"))
                    {
                        playerAnimator.Play("ArmlessWalkingLeft");
                    }
                    else
                    {
                        playerAnimator.Play("WalkingLeft");
                    }
                }
                else if (angle >= -135 && angle < -45)
                {
                    if (playerAnimator.GetBool("IsAttacking"))
                    {
                        playerAnimator.Play("ArmlessWalkingDown");
                    }   
                    else
                    {
                        playerAnimator.Play("WalkingDown");
                    }
                }
            }
            else
            {
                playerAnimator.SetBool("IsIdle", true);
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
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsDamaged", true);
        }
    }

    /// <summary>
    /// Установка значения false для параметра IsDamaged
    /// </summary>
    public void ResetDamageState()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsDamaged", false);
        }
    }

    public void EnableAttackingState()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsAttacking", true);
        }
    }

    public void DisableAttackingState()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsAttacking", false);
        }
    }
    void OnDisable()
    {
        playerHealthManager.OnTakeDamage -= OnDamageTaken;
        WeaponManager.Instance.OnAttack -= EnableAttackingState;
    }
}