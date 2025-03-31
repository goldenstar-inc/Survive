using UnityEngine;
using UnityEngine.UI;
using static EquippedGun;
public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] AmmoHandler playerAmmoHandler;
    [SerializeField] Image[] ammo;

    public void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            ammo[i].enabled = i < currentAmmo;
        }
    }

    public void Initialize()
    {
        UpdateAmmoBar(0, 6);
    }

    void OnEnable()
    {
        playerAmmoHandler.OnConsume += UpdateAmmoBar;
        playerAmmoHandler.OnCollect += UpdateAmmoBar;
        
    }
    void OnDisable()
    {
        playerAmmoHandler.OnConsume -= UpdateAmmoBar;
        playerAmmoHandler.OnCollect -= UpdateAmmoBar;
    }
}
