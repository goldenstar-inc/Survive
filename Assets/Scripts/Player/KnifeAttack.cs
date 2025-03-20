using System.Collections;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за атаку холодным оружием
/// </summary>
public class KnifeAttack : MonoBehaviour
{
    /// <summary>
    /// Переменная, отвечающая за текущий статус атаки
    /// </summary>
    private bool isAttacking = false;

    /// <summary>
    /// Объект, содержащий анимацию
    /// </summary>
    public GameObject attackingRange;

    /// <summary>
    /// Аниматор, отвечающий за анимацию атаки
    /// </summary>
    public Animator attackingAnimator;

    /// <summary>
    /// Метод, меняющий видимость объекта
    /// </summary>
    private void ChangeVisibility() => attackingRange.SetActive(isAttacking);

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            ChangeVisibility();
        }

        if (isAttacking && attackingAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            isAttacking = false;
            ChangeVisibility();
        }
    }
}