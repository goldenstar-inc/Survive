using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public IAttackScript currentWeapon { get; private set; }

    public void SetCurrentWeapon(IAttackScript selectedWeapon)
    {
        currentWeapon = selectedWeapon;
    }

    public void Attack()
    {
        currentWeapon.Attack();
    }
}
