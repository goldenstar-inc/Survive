using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHandler : MonoBehaviour
{
    public static AmmoHandler Instance { get; set; }

    public Image[] ammoImages;

    public int currentAmmo { get; private set; }

    public int maxAmmo { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            currentAmmo = 0;
            maxAmmo = ammoImages.Length;
            ShowCurrentAmmo();
        }
        else
        {
            Destroy(Instance);
        }
    }
    
    public void ConsumeAmmo()
    {
        currentAmmo = Math.Max(0, currentAmmo - 1);
        ShowCurrentAmmo();
    }

    public void CollectAmmo(int amount)
    {
        currentAmmo = Math.Min(maxAmmo, currentAmmo + amount);
        ShowCurrentAmmo();
    }

    public void ShowCurrentAmmo()
    {
        for (int i = 0; i < ammoImages.Length; i++)
        {
            ammoImages[i].enabled = i < currentAmmo;
        }
    }
}
