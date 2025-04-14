using System;
using UnityEngine;
using UnityEngine.UI;
using static Ammo;
public class AmmoHandler : MonoBehaviour
{
    public int currentAmmo { get; private set; }
    public int maxAmmo { get; private set; }
    public event Action<int, int> OnConsume;
    public event Action<int, int> OnCollect;
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="maxAmmo">Максимальный боезапас</param>
    public void Init(int maxAmmo)
    {
        currentAmmo = 0;
        this.maxAmmo = maxAmmo;
    }
    
    public void ConsumeAmmo()
    {
        currentAmmo = Math.Max(0, currentAmmo - 1);
        OnConsume?.Invoke(currentAmmo, maxAmmo);
    }

    public void CollectAmmo(int amount)
    {
        currentAmmo = Math.Min(maxAmmo, currentAmmo + amount);
        OnCollect?.Invoke(currentAmmo, maxAmmo);
    }
}
