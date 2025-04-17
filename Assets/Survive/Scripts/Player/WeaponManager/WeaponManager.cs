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
    public event Action OnAttack;
    public event Action<CreatureType> OnKill;

    private Animator animator;
    private AmmoHandler ammoHandler;
    private Transform attackStartPoint;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="animator">Аниматор оружия</param>
    /// <param name="ammoHandler">Скрипт, управляющий боезопасом</param>
    /// <param name="attackStartPoint">Стартовая точка для атаки</param>
    public void Init(Animator animator, AmmoHandler ammoHandler, Transform attackStartPoint)
    {
        this.animator = animator;
        this.ammoHandler = ammoHandler;
        this.attackStartPoint = attackStartPoint;
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

    private void OnDestroy()
    {
        OnAttack = null;
    }

    public void Kill(CreatureType enemyType)
    {
        OnKill.Invoke(enemyType);
    }
}
