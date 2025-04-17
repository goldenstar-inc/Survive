using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    public void SpawnBullet(GameObject prefab, Transform shotStartPoint, int damage, float velocity, float lifeTime, PlayerDataProvider playerData)
    {
        GameObject bulletInstance = Instantiate(prefab, shotStartPoint.position, shotStartPoint.rotation);
        Bullet bullet = bulletInstance.GetComponentInChildren<Bullet>();
        bullet?.Initialize(damage, playerData);
        Rigidbody2D bulletPrefabRB = bulletInstance.GetComponentInChildren<Rigidbody2D>();
        bulletPrefabRB.linearVelocity = shotStartPoint.right * velocity;
        Destroy(bulletInstance, lifeTime);
    }
}