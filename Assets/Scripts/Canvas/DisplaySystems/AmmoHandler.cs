using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHandler : MonoBehaviour
{
    [SerializeField] public int maxAmmo;

    public int currentAmmo { get; private set; }
    public event Action<int, int> OnConsume;
    public event Action<int, int> OnCollect;

    void Start()
    {
        currentAmmo = 0;
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
