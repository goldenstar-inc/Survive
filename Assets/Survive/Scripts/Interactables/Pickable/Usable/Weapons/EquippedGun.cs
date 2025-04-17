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
    private PlayerDataProvider playerData;

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
    private AudioClip shotSound;
    private float timeSinceLastShot = 0f;
    private WeaponManager weaponManager;
    public void Init(RangedWeaponItemData data, PlayerDataProvider playerData)
    {
        this.playerData = playerData;
        weaponManager = playerData?.WeaponManager;
        shotStartPoint = weaponManager?.GetAttackStartPoint();
        Animator weaponAnimator = weaponManager?.GetWeaponAnimator();
        ammoHandler = weaponManager?.GetAmmoHandlerScript();
        if (weaponAnimator.runtimeAnimatorController != null)
        {
            weaponAnimator.runtimeAnimatorController = data.Animator;
        }

        this.data = data;
        bulletPrefab = data.BulletPrefab;
        damage = data.Damage;
        bulletVelocity = data.BulletVelocity;
        bulletLifeTime = data.BulletLifeTime;
        shotSound = data.ShotSound;
        attackCooldown = data.AttackCooldown;
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
                    playerData.WeaponManager.PlayAttackAnimation();
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
            BulletSpawner.Instance.SpawnBullet(bulletPrefab, shotStartPoint, damage, bulletVelocity, bulletLifeTime, playerData);
            timeSinceLastShot = Time.time;
            playerData.SoundController?.PlayAudioClip(shotSound);
        }
    }
}
