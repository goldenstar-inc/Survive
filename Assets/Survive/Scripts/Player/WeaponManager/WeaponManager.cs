using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за управление атакой игрока
/// </summary>

[RequireComponent(typeof(Animator))]
public class WeaponManager : MonoBehaviour
{
    public event Action<WeaponItemData> OnAttack;
    public event Action<CreatureType, HealthHandler> OnKill;

    private Animator animator;
    private AmmoHandler ammoHandler;
    private Transform attackStartPoint;
    private float timeSinceLastAttack = int.MinValue;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="animator">Аниматор оружия</param>
    /// <param name="ammoHandler">Класс, управляющий боезопасом</param>
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

    /// <summary>
    /// Метод, возвращающий аниматор оружия
    /// </summary>
    /// <returns>Аниматор оружия</returns>
    public Animator GetWeaponAnimator()
    {
        return animator;
    }

    /// <summary>
    /// Метод, возвращающий класс, отвечающий за боеприпасы
    /// </summary>
    /// <returns>Класс, отвечающий за боеприпасы</returns>
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
        }
    }

    /// <summary>
    /// Метод, зажигающий событие, что существо был убит
    /// </summary>
    /// <param name="creatureType">Тип существа</param>
    /// <param name="killedCreature">Класс, отвечающий за здоровье существа</param>
    public void Kill(CreatureType creatureType, HealthHandler killedCreature)
    {
        OnKill?.Invoke(creatureType, killedCreature);
    }

    /// <summary>
    /// Метод атаки
    /// </summary>
    /// <param name="weaponItemData">Данные об оружии</param>
    public void Attack(WeaponItemData weaponItemData)
    {
        PlayAttackAnimation();
        OnAttack?.Invoke(weaponItemData);   
        timeSinceLastAttack = Time.time;
    }

    /// <summary>
    /// Метод, возвращающий время с последней атаки
    /// </summary>
    /// <returns>Время с последней атаки</returns>
    public float GetTimeSinceLastAttack()
    {
        return timeSinceLastAttack;
    }

    /// <summary>
    /// Метод, вызывающийся при уничтожении объекта
    /// </summary>
    private void OnDestroy()
    {
        OnAttack = null;
    }
}
