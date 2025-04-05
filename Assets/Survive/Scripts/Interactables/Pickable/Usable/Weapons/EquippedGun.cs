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
    /// Информация о взаимодействующем персонаже
    /// </summary>
    private IPlayerDataProvider playerData;

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
    public void Initialize(RangedWeaponItemData data, IPlayerDataProvider playerData)
    {
        this.playerData = playerData;

        this.data = data;
        shotStartPoint = WeaponManager.Instance.GetAttackStartPoint();
        bulletPrefab = data.BulletPrefab;
        damage = data.Damage;
        bulletVelocity = data.BulletVelocity;
        bulletLifeTime = data.BulletLifeTime;
        shotSound = data.ShotSound;
        attackCooldown = data.AttackCooldown;

        Animator weaponAnimator = WeaponManager.Instance.GetWeaponAnimator();
        ammoHandler = WeaponManager.Instance.GetAmmoHandlerScript();

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
        if (ammoHandler != null)
        {
            if (Time.time - timeSinceLastShot > attackCooldown)
            {
                int currentAmmo = ammoHandler.currentAmmo;

                if (currentAmmo > 0)
                {
                    Shoot();
                    ammoHandler.ConsumeAmmo();
                    WeaponManager.Instance.PlayAttackAnimation();
                    return true;
                }
            }
        }
        return false;
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
            playerData.SoundController?.PlaySound(shotSound);
        }
    }
}
