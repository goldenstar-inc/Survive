using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerSetting playerSetting;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private AmmoHandler ammoHandler;
    [SerializeField] private MoneyHandler moneyHandler;
    [SerializeField] private SoundController soundController;
    public override void InstallBindings()
    {
        Container.Bind<PlayerDataProvider>()
        .To<PlayerDataProvider>()
        .AsSingle()
        .WithArguments(
            playerSetting, 
            healthManager, 
            ammoHandler, 
            moneyHandler, 
            soundController
            );
    }
}
