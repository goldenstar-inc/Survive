using UnityEngine;

[CreateAssetMenu(fileName = "PistolUseLogic", menuName = "Items/Pistol Use Logic")]
public class PistolUseLogic : ItemUseLogic
{
    [Tooltip("Prefab пули")] 
    public GameObject bulletPrefab;

    [Tooltip("Точка старта пули (должна быть назначена динамически)")] 
    public Transform bulletStartPoint;

    [Tooltip("Скорость пули")] 
    public float bulletSpeed = 20f;

    [Tooltip("Время жизни пули")] 
    public float bulletLifeTime = 5f;

    public override void Use()
    {
        if (bulletPrefab != null && bulletStartPoint != null)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, bulletStartPoint.position, bulletStartPoint.rotation);
            Rigidbody2D bulletRB = bulletInstance.GetComponent<Rigidbody2D>();
            if (bulletRB != null)
            {
                bulletRB.linearVelocity = bulletStartPoint.right * bulletSpeed;
            }
            Destroy(bulletInstance, bulletLifeTime);
        }
        else
        {
            Debug.LogWarning("Bullet prefab or start point is not assigned!");
        }
    }
}
