using SettingsMenu;
using UnityEngine;
using Zenject;

internal class CoreInstaller : MonoInstaller
{
    [SerializeField] private PreferencesConfig _preferencesConfig;

    public override void InstallBindings()
    {
        BindSettings();
    }

    private void BindSettings()
    {
        Container.Bind<PreferencesConfig>().FromInstance(_preferencesConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<SettingsManager>().FromNew().AsSingle().NonLazy();
    }
}