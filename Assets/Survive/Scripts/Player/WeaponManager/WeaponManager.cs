using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление атакой игрока
/// </summary>

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AmmoHandler))]
public class WeaponManager : MonoBehaviour
    {
    [SerializeField] Transform attackStartPoint;
    public event Action OnAttack;
    public static WeaponManager Instance { get; private set; }
    private Animator animator;
    private AmmoHandler ammoHandler;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            animator = GetComponent<Animator>();
            ammoHandler = GetComponent<AmmoHandler>();
        }
        else
        {
            Destroy(gameObject);
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
    public AmmoHandler GetAmmoHandlerScript()
    {
        return ammoHandler;
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
