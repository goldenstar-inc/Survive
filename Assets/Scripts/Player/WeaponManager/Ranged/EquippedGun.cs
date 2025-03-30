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
    private Animator gunAnimator;
    private Transform shotStartPoint;
    private RangedWeaponItemData data;
    private GameObject bulletPrefab;
    private int damage;
    private float bulletVelocity;
    private float bulletLifeTime;
    private SoundType shotSound;
    private float timeSinceLastShot = 0f;
    public void Initialize(RangedWeaponItemData data)
    {
        shotStartPoint = WeaponManager.Instance.GetAttackStartPoint();
        gunAnimator = data.Animator;
        this.data = data;
        bulletPrefab = data.BulletPrefab;
        damage = data.Damage;
        bulletVelocity = data.BulletVelocity;
        bulletLifeTime = data.BulletLifeTime;
        shotSound = data.ShotSound;
    }

    /// <summary>
    /// Метод, контроллирующий атаку из пистолета
    /// </summary>
    public bool Use()
    {
        if (Time.time - timeSinceLastShot > data.AttackCooldown)
        {
            Shoot();
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
            EnableAttacking();
        }
    }

    /// <summary>
    /// Метод, включающий атакующее положение игрока
    /// </summary>
    public void EnableAttacking()
    {
        gunAnimator.SetTrigger("IsAttacking");
    }
}
