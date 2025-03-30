using UnityEngine;

[CreateAssetMenu(fileName = "Pistol", menuName = "Items/Pistol Item Data")]
public class PistolItemData: WeaponItemData
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletStartPoint;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator pistolAnimator;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime;
    public override void Use()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (bulletPrefab != null && bulletStartPoint != null)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, bulletStartPoint.transform.position, bulletStartPoint.transform.rotation);
            Rigidbody2D bulletPrefabRB = bulletInstance.GetComponentInChildren<Rigidbody2D>();
            bulletPrefabRB.linearVelocity = bulletStartPoint.transform.right * bulletSpeed;
            Destroy(bulletInstance, bulletLifeTime);
            SoundController.Instance.PlaySound(SoundType.PistolShot, SoundController.Instance.weaponAudioSource);
        }
    }
}

