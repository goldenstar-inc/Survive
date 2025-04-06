using UnityEngine;
public class PlayerContext : MonoBehaviour, IPlayerDataProvider
{
    [Tooltip("Конфиг игрока")]
    [SerializeField] PlayerSetting playerSetting;

    [Tooltip("Cкрипт управления боеприпасами персонажа")]
    [SerializeField] HealthManager healthManager;

    [Tooltip("Cкрипт управления боеприпасами персонажа")]
    [SerializeField] AmmoHandler ammoHandler;

    [Tooltip("Cкрипт управления денежным балансом персонажа")]
    [SerializeField] MoneyHandler moneyHandler;

    [Tooltip("Cкрипт управления звуком персонажа")]
    [SerializeField] SoundController soundController;
    public PlayerSetting PlayerSetting => playerSetting;
    public HealthManager HealthManager => healthManager;
    public AmmoHandler AmmoHandler => ammoHandler;
    public MoneyHandler MoneyHandler => moneyHandler;
    public SoundController SoundController => soundController;
    
}

