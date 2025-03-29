using UnityEngine;

/// <summary>
/// Класс, ответственный за анимацию оружия в руках у игрока
/// </summary>
public class WeaponAnimator : MonoBehaviour
{
    /// <summary>
    /// Аниматор оружия
    /// </summary>
    private Animator animator;
    
    public WeaponAnimator(Animator animator)
    {
        this.animator = animator;
    }

    /// <summary>
    /// Метод, проигрывающий анимацию атаки
    /// </summary>
    public void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}
