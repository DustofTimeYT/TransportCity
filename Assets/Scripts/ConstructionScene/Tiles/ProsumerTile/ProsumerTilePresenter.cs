using AbsMoveTile;
using ProsumerTile;
using System.Collections.Generic;
using UnityEngine;

public class ProsumerTilePresenter : AbsMoveTilePresenter<ProsumerTileModel>, IConsumer, IProducer
{
    public ProsumerTilePresenter(Vector2Int coords, int rotationAngle, AbsTileConfig config, Transform parent) : base(coords, rotationAngle, config, parent)
    {
        if (config.TileType != TileType.Prosumer) return;

        _model = new ProsumerTileModel(config, rotationAngle, coords);

        InstantiateView(config.TilePref, rotationAngle, parent);
    }

    public List<ProductType> GetConsumerMaterials()
    {
        return _model.ConsumerProduct;
    }

    public bool CheckConsumerMaterial(ProductType product)
    {
        return _model.ConsumerProduct.Contains(product);
    }

    public List<ProductType> GetProducerMaterials()
    {
        return _model.ProducerProduct;
    }

    public void HandOverCargo(List<ICargoItem> cargo, RoutePresenter route)
    {
        Debug.Log("Unload");
        route.RouteIsOver();
    }

    public List<ICargoItem> GetCargo(RoutePresenter route)
    {
        List <ICargoItem> cargos = new List <ICargoItem>();

        for (int i = 1; i == route.GetCargoAmount(); i++)
        {
            cargos.Add(new CargoItem(route.GetProduct()));
        }
        Debug.Log("Load");
        return cargos;
    }
}
