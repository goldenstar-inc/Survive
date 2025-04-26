using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private HealthHandler zombieHealthManager;

    /// <summary>
    /// Аниматор, отвечающий за анимирование зомби
    /// </summary>
    private Animator zombieAnimator;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    public void Init(HealthHandler healthManager, Animator zombieAnimator)
    {
        healthManager.OnDamageTaken += EnableDamagedAnimation;
        this.zombieAnimator = zombieAnimator;
    }

    /// <summary>
    /// Метод, обновляющий анимацию игрока
    /// </summary>
    public void UpdateMovementAnimation(Vector3 input)
    {
        if (input != null && zombieAnimator != null && zombieAnimator.GetBool("IsDamaged") == false)
        {
            if (input.x != 0 || input.y != 0)
            {
                zombieAnimator.SetBool("IsIdle", false);
                if (input.x != 0)
                {
                    if (input.x > 0)
                    {
                        zombieAnimator.Play("ZombieWalkingRight");
                    }
                    else
                    {
                        zombieAnimator.Play("ZombieWalkingLeft");
                    }
                }
                else
                {
                    if (input.y > 0)
                    {
                        zombieAnimator.Play("ZombieWalkingUp");
                    }
                    else
                    {
                        zombieAnimator.Play("ZombieWalkingDown");
                    }
                }
            }
            else
            {
                zombieAnimator.SetBool("IsIdle", true);
            }
        }
    }

    /// <summary>
    /// Метод, вызывающийся при получении урона
    /// </summary>
    /// <param name="currentHealth">Текущее количество очков здоровья</param>
    /// <param name="maxHealth">Максимальное количество очков здоровья</param>
    /// <param name="healthComponent">Информация, свзяанная со здоровьем</param>
    public void EnableDamagedAnimation(int currentHealth, int maxHealth, HealthComponent healthComponent)
    {
        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("IsDamaged", true);
        }
    }

    /// <summary>
    /// Установка значения false для параметра IsDamaged
    /// </summary>
    public void ResetDamageState()
    {
        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("IsDamaged", false);
        }
    }
    void OnDisable()
    {
        if (zombieHealthManager != null)
        {
            zombieHealthManager.OnDamageTaken -= EnableDamagedAnimation;
        }
    }
}
