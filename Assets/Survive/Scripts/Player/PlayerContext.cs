using UnityEngine;
public class PlayerContext : MonoBehaviour, IPlayerDataProvider
{
    [Tooltip("Cкрипт управления боеприпасами персонажа")]
    [SerializeField] HealthManager healthManager;

    [Tooltip("Cкрипт управления боеприпасами персонажа")]
    [SerializeField] AmmoHandler ammoHandler;

    [Tooltip("Cкрипт управления денежным балансом персонажа")]
    [SerializeField] MoneyHandler moneyHandler;

    [Tooltip("Cкрипт управления звуком персонажа")]
    [SerializeField] SoundController soundController;
    public HealthManager HealthManager => healthManager;
    public AmmoHandler AmmoHandler => ammoHandler;
    public MoneyHandler MoneyHandler => moneyHandler;
    public SoundController SoundController => soundController;
}

