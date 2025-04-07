using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Transform cameraTransform;
    public override void InstallBindings()
    {
        Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle().WithArguments(cameraTransform);
    }
}
