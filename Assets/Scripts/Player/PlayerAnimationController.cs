using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Класс, отвечающий за воспроизведение анимаций игрока
/// </summary>
public class PlayerAnimationController : MonoBehaviour, IDamageObserver
{
    private Animator animator;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Метод, обновляющий анимацию игрока
    /// </summary>
    public void UpdateMovementAnimation(Vector3 input)
    {
        if (input != null && animator != null && animator.GetBool("IsDamaged") == false)
        {            
            if (input.x != 0)
            {
                animator.SetBool("IsIdle", false);
                FlipModel(input);
                animator.Play("WalkingSideways");
            }
            else if(input.y != 0)
            {
                animator.SetBool("IsIdle", false);
                if (input.y > 0)
                {
                    animator.Play("WalkingUp");
                }
                else
                {
                    animator.Play("WalkingDown");
                }
            }
            else
            {
                animator.SetBool("IsIdle", true);
            }
        }
    }

    /// <summary>
    /// Переворачивает спрайт по оси Ox
    /// </summary>
    /// <param name="input">Клавиатурный ввод</param>
    private void FlipModel(Vector3 input) 
    {
        if (input.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
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
}
