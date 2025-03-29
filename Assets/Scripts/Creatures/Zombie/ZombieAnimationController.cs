using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    [SerializeField] HealthManager zombieHealthManager;

    /// <summary>
    /// Аниматор, отвечающий за анимирование зомби
    /// </summary>
    private Animator zombieAnimator;

    private Rigidbody2D rb;


    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
    public void OnDamageTaken(int currentHealth, int maxHealth)
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

    void OnEnable()
    {
        zombieHealthManager.OnTakeDamage += OnDamageTaken;
    }
    void OnDisable()
    {
        zombieHealthManager.OnTakeDamage -= OnDamageTaken;
    }
}
