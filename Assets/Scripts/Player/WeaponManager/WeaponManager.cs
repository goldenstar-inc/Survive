using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    [SerializeField] Transform attackStartPoint;
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

    /// <summary>
    /// Возврает точку атаки
    /// </summary>
    /// <returns>Точку атаки</returns>
    public Transform GetAttackStartPoint()
    {
        return attackStartPoint;
    }
}
