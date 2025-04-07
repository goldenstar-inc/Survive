using UnityEngine;
public class PlayerContext : PlayerDataProvider
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
    public override PlayerSetting PlayerSetting => playerSetting;
    public override HealthManager HealthManager => healthManager;
    public override AmmoHandler AmmoHandler => ammoHandler;
    public override MoneyHandler MoneyHandler => moneyHandler;
    public override SoundController SoundController => soundController;
    
}

