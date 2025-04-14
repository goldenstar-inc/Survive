using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отображающий количество патронов игрока
/// </summary>
public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] Image[] ammo;
    private AmmoHandler ammoHandler;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="ammoHandler">Скрипт, управляющий боезапасом</param>
    public void Init(AmmoHandler ammoHandler)
    {
        this.ammoHandler = ammoHandler;
        ammoHandler.OnConsume += UpdateAmmoBar;
        ammoHandler.OnCollect += UpdateAmmoBar;
        UpdateAmmoBar(0, ammoHandler.maxAmmo);
    }
    public void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            ammo[i].enabled = i < currentAmmo;
        }
    }
    void OnDisable()
    {
        if (ammoHandler != null)
        {
            ammoHandler.OnConsume -= UpdateAmmoBar;
            ammoHandler.OnCollect -= UpdateAmmoBar;
        }
    }
}
