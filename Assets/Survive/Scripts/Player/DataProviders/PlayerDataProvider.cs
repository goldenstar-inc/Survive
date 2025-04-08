using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

/// <summary>
/// Интерфейс, содержащий ссылки на компоненты игрока
/// </summary>
public class PlayerDataProvider : MonoBehaviour, IPlayerSettingProvider, IHealthProvider, IAmmoProvider, IMoneyProvider, ISoundProvider, IWeaponProvider
{
    [SerializeField] PlayerSetting playerSetting;
    [SerializeField] HealthManager healthManager;
    [SerializeField] AmmoHandler ammoHandler;
    [SerializeField] MoneyHandler moneyHandler;
    [SerializeField] SoundController soundController;
    [SerializeField] WeaponManager weaponManager;
    public PlayerSetting PlayerSetting => playerSetting;
    public HealthManager HealthManager => healthManager;
    public AmmoHandler AmmoHandler => ammoHandler;
    public MoneyHandler MoneyHandler => moneyHandler;
    public SoundController SoundController => soundController;
    public WeaponManager WeaponManager => weaponManager;
}
