using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private Button jumpButton; // Привязка кнопки из инспектора
    [SerializeField] private Joystick joystick; // Привязка джойстика из инспектора

    public override void InstallBindings()
    {
#if UNITY_ANDROID || UNITY_IOS
        Container.Bind<Button>().FromInstance(jumpButton).AsSingle();
        Container.Bind<Joystick>().FromInstance(joystick).AsSingle();
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
#else
        Container.Bind<Button>().FromInstance(jumpButton).AsSingle();
        Container.Bind<Joystick>().FromInstance(joystick).AsSingle();
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
        //Container.Bind<IInput>().To<PCInput>().AsSingle();
#endif
    }
}
