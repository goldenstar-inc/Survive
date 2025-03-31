using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public event Action OnAttack;
    public static WeaponManager Instance { get; private set; }
    [SerializeField] Transform attackStartPoint;
    private Animator animator;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }
        else
        {
            Destroy(Instance);
        }
    }

    /// <summary>
    /// Возврает точку атаки
    /// </summary>
    /// <returns>Точка атаки</returns>
    public Transform GetAttackStartPoint()
    {
        return attackStartPoint;
    }

    public Animator GetWeaponAnimator()
    {
        return animator;
    }

    /// <summary>
    /// Метод, проигрывающий анимацию атаки
    /// </summary>
    public void PlayAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            OnAttack?.Invoke();
        }
    }
}
