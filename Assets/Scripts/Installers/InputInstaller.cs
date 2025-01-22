using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private Button jumpButton;
    [SerializeField] private Joystick joystick;

    public override void InstallBindings() // change input (mobile or PC)
    {
#if UNITY_ANDROID || UNITY_IOS
        Container.Bind<Button>().FromInstance(jumpButton).AsSingle();
        Container.Bind<Joystick>().FromInstance(joystick).AsSingle();
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
#else
        Container.Bind<Button>().FromInstance(jumpButton).AsSingle();
        Container.Bind<Joystick>().FromInstance(joystick).AsSingle();
        Container.Bind<IInput>().To<PCInput>().AsSingle();
#endif
    }
}
