using Grid;
using PathFindAlgo;
using UnityEngine;
using Zenject;


public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private TilesConfig _tilesConfig;
    [SerializeField] private ConstructionSystem_V _constructionSystem_V;

    public override void InstallBindings()
    {
        BindEventBus();
        BindGrid();
        BindPathFind();
        BindConstructionSystem();
    }

    private void BindGrid()
    {
        Container.Bind<GridConfig>().FromInstance(_gridConfig).AsSingle();
        Container.Bind<TilesConfig>().FromInstance(_tilesConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<GridGenerator>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<GridPresenter>().FromNew().AsSingle().NonLazy();
        BindGridLayers();
    }

    private void BindGridLayers()
    {
        Container.BindInterfacesAndSelfTo<MoveGridLayer>().FromNew().AsSingle();

    }

    private void BindPathFind()
    {
        Container.Bind<ISurroundingTilesFinder>().To<SurroundingTilesFinder>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<PathFinding>().FromNew().AsSingle().NonLazy();
    }

    private void BindConstructionSystem()
    {
        Container.BindInterfacesAndSelfTo<ConstructionSystem_P>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ConstructionSystem_V>().FromInstance(_constructionSystem_V).AsSingle();
    }

    private void BindEventBus()
    {
    }
}

