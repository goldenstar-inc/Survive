using System;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Класс, отвечающий за стрельбу из пистолета
/// </summary>
public class EquippedGun : IUseScript
{
    /// <summary>
    /// Конфиг оружия
    /// </summary>
    private RangedWeaponItemData data;
    private Animator playerAnimator;
    private AmmoHandler ammoHandler;
    private Transform shotStartPoint;
    private GameObject bulletPrefab;
    private int damage;
    private float bulletVelocity;
    private float bulletLifeTime;
    private float attackCooldown;
    private SoundType shotSound;
    private float timeSinceLastShot = 0f;
    public void Initialize(RangedWeaponItemData data)
    {
        shotStartPoint = WeaponManager.Instance.GetAttackStartPoint();
        this.data = data;
        bulletPrefab = data.BulletPrefab;
        damage = data.Damage;
        bulletVelocity = data.BulletVelocity;
        bulletLifeTime = data.BulletLifeTime;
        shotSound = data.ShotSound;
        attackCooldown = data.AttackCooldown;

        Animator weaponAnimator = WeaponManager.Instance.GetWeaponAnimator();
        
        if (weaponAnimator != null)
        {
            weaponAnimator.runtimeAnimatorController = data.Animator;
        }
    }

    /// <summary>
    /// Метод, контроллирующий атаку из пистолета
    /// </summary>
    public bool Use()
    {
        if (Time.time - timeSinceLastShot > attackCooldown)
        {
            Shoot();
            WeaponManager.Instance.PlayAttackAnimation();
            return true;
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.NotReady, SoundController.Instance.weaponAudioSource);
            return false;
        }
    }

    /// <summary>
    /// Метод, отвечающий за стрельбу из пистолета
    /// </summary>
    public void Shoot()
    {
        if (bulletPrefab != null)
        {
            BulletSpawner.Instance.SpawnBullet(bulletPrefab, shotStartPoint, damage, bulletVelocity, bulletLifeTime);
            timeSinceLastShot = Time.time;
            SoundController.Instance.PlaySound(shotSound, SoundController.Instance.weaponAudioSource);
        }
    }
}
