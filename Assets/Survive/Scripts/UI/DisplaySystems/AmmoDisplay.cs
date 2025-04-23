using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отображающий количество патронов игрока
/// </summary>
public class AmmoDisplay : MonoBehaviour
{    
    private AmmoHandler ammoHandler;
    private TextMeshProUGUI ammoAmountPlaceholder;
    
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="ammoHandler">Скрипт, управляющий боезапасом</param>
    public void Init(AmmoHandler ammoHandler, TextMeshProUGUI ammoAmountPlaceholder)
    {
        this.ammoAmountPlaceholder = ammoAmountPlaceholder;
        this.ammoHandler = ammoHandler;
        ammoHandler.OnConsume += UpdateAmmoBar;
        ammoHandler.OnCollect += UpdateAmmoBar;
        UpdateAmmoBar(ammoHandler.currentAmmo, ammoHandler.maxAmmo);
    }
    public void UpdateAmmoBar(int currentAmmo, int maxAmmo)
    {
        ammoAmountPlaceholder.text = $"{currentAmmo}";
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
