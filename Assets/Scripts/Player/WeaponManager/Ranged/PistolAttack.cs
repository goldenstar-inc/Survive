using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Класс, отвечающий за стрельбу из пистолета
/// </summary>
public class PistolAttack : MonoBehaviour, IAttackScript
{
    /// <summary>
    /// Компонент AmmoHandler
    /// </summary>
    public AmmoHandler ammoHandler;

    /// <summary>
    /// Аниматор игрока
    /// </summary>
    public Animator playerAnimationController;

     /// <summary>
    /// Аниматор пистолета
    /// </summary>
    public Animator pistolAnimatorController;

    /// <summary>
    /// Начальное положение пули
    /// </summary>
    public GameObject shotStartPoint;

    /// <summary>
    /// Префаб пули
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Скорость стрельбы из пистолета
    /// </summary>
    public float attackSpeed = 20f;

    /// <summary>
    /// Время с последнего выстрела
    /// </summary>
    private float timeSinceLastShot;

    /// <summary>
    /// Скорость пули
    /// </summary>
    private float bulletSpeed = 40f;

    /// <summary>
    /// Время существования пули
    /// </summary>
    private float lifeTime = 10f;

    /// <summary>
    /// Метод, срабатывющий при инициализации объекта
    /// </summary>
    void Start()
    {
        if (playerAnimationController == null)
        {
            Debug.LogWarning("PlayerAnimationController not loaded");
        }

        if (shotStartPoint == null)
        {
            Debug.LogWarning("ShotStartPoint not loaded");
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning("BulletPrefab not loaded");
        }

        if (attackSpeed == 0f)
        {
            Debug.LogWarning("AttackSpeed not set");
        }
    }

    /// <summary>
    /// Метод, контроллирующий атаку из пистолета
    /// </summary>
    public void Attack()
    {
        if (Time.time - timeSinceLastShot > attackSpeed)
        {
            if (ammoHandler.currentAmmo > 0)
            {
                EnableAttacking();
                Shoot();
            }
            else
            {
                SoundController.Instance.PlaySound(SoundType.EmptyMag, SoundController.Instance.weaponAudioSource);
            }
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.NotReady, SoundController.Instance.weaponAudioSource);
        }
    }

    /// <summary>
    /// Метод, отвечающий за стрельбу из пистолета
    /// </summary>
    public void Shoot()
    {
        if (bulletPrefab != null && shotStartPoint != null)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, shotStartPoint.transform.position, shotStartPoint.transform.rotation);
            Rigidbody2D bulletPrefabRB = bulletInstance.GetComponentInChildren<Rigidbody2D>();
            bulletPrefabRB.linearVelocity = shotStartPoint.transform.right * bulletSpeed;
            timeSinceLastShot = Time.time;
            ammoHandler.ConsumeAmmo();
            Destroy(bulletInstance, lifeTime);
            SoundController.Instance.PlaySound(SoundType.PistolShot, SoundController.Instance.weaponAudioSource);
        }
    }

    /// <summary>
    /// Метод, включающий атакующее положение игрока
    /// </summary>
    public void EnableAttacking()
    {
        playerAnimationController.SetBool("IsAttacking", true);
        pistolAnimatorController.SetBool("IsAttacking", true);
    }

    /// <summary>
    /// Метод, отключающий атакующее положение игрока
    /// </summary>
    public void DisableAttack() 
    {
        playerAnimationController.SetBool("IsAttacking", false);
        pistolAnimatorController.SetBool("IsAttacking", false);
    }
}
