using UnityEngine;
using Zenject;
using SettingsMenu;


public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField] private SettingsMenuView _settingsMenuView;

    public override void InstallBindings()
    {
        BindSettingsMenu();
    }

    private void BindSettingsMenu()
    {
        Container.BindInterfacesAndSelfTo<SettingsMenuPresenter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<SettingsMenuView>().FromInstance(_settingsMenuView).AsSingle();
    }


}

