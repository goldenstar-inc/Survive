using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отображающий количество патронов игрока
/// </summary>
public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] Image[] ammo;
    [SerializeField] AmmoHandler playerAmmoHandler;
    public void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            ammo[i].enabled = i < currentAmmo;
        }
    }
    public void Start()
    {
        UpdateAmmoBar(0, playerAmmoHandler.maxAmmo);
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
