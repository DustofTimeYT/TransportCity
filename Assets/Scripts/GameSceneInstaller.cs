using Grid;
using ContractSystem;
using PathFindAlgo;
using UnityEngine;
using Zenject;
using GarageManager;
using OrderManager;
using Bank;


public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private BankConfig _bankConfig;
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private TilesConfig _tilesConfig;
    [SerializeField] private TransportCatalog _transportCatalog;

    [SerializeField] private BankView _bankView;
    [SerializeField] private ConstructionSystem_V _constructionSystem_V;
    [SerializeField] private ContractBoardView _contractBoardView;
    [SerializeField] private TransportStoreView _transportStoreView;
    [SerializeField] private OrderMenuView _orderMenuView;
    public override void InstallBindings()
    {
        BindEventBus();
        BindBank();
        BindGrid();
        BindPathFind();
        BindConstructionSystem();
        BindOrderManager();
        BindContractBoard();
        BindTransportStore();
    }

    private void BindBank()
    {
        Container.Bind<BankConfig>().FromInstance(_bankConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<BankPresenter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<BankView>().FromInstance(_bankView).AsSingle();
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
        Container.BindInterfacesAndSelfTo<MoveGridLayer>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ConsumerGridLayer>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ProducerGridLayer>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GarageGridLayer>().FromNew().AsSingle().NonLazy();
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

    private void BindContractBoard()
    {
        Container.BindInterfacesAndSelfTo<ContractGenerator>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ContractBoardPresenter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ContractBoardView>().FromInstance(_contractBoardView).AsSingle();
    }

    private void BindOrderManager()
    {
        Container.BindInterfacesAndSelfTo<OrderManagerPresenter>().FromNew().AsSingle();
        BindOrderMenu();
    }

    private void BindOrderMenu()
    {
        Container.BindInterfacesAndSelfTo<OrderMenuPresenter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<OrderMenuView>().FromInstance(_orderMenuView).AsSingle();

    }

    private void BindTransportStore()
    {
        Container.Bind<TransportCatalog>().FromInstance(_transportCatalog).AsSingle();
        Container.Bind<IGarageManager>().To<GarageManagerPresenter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<TransportStorePresenter>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<TransportStoreView>().FromInstance(_transportStoreView).AsSingle();
    }

    private void BindEventBus()
    {
    }
}

