using UnityEditor;
using UnityEngine;

public static class UseScriptFabric
{
    public static IUseScript GetUseScript(PickableItemData itemData, PlayerDataProvider playerData)
    {
        if (itemData is RangedWeaponItemData rangedWeaponItemData)
        {
            EquippedGun rangedWeapon = new EquippedGun();
            rangedWeapon.Init(rangedWeaponItemData, playerData);
            return rangedWeapon;
        }
        else if (itemData is MeleeWeaponItemData meleeWeaponItemData)
        {
            EquippedMeleeWeapon meleeWeapon = new EquippedMeleeWeapon();
            meleeWeapon.Init(meleeWeaponItemData, playerData);
            return meleeWeapon;
        }
        else if (itemData is HealableItemData healableItemData)
        {
            EquippedHealableItem healableItem = new EquippedHealableItem();
            healableItem.Init(healableItemData, playerData);
            return healableItem;
        }
        else
        {
            return null;
        }
    }
}
