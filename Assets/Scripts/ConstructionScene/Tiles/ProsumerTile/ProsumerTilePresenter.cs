using AbsTile;
using ProsumerTile;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProsumerTilePresenter : AbsTilePresenter, IConsumer, IProducer
{
    readonly private ProsumerTileModel _model;
    public ProsumerTilePresenter(Vector2Int coords, AbsTileConfig config, Transform parent)
    {
        if (config.TileType != TileType.Prosumer) return;
        _model = new ProsumerTileModel(config, coords);
        InstantiateView(config.TilePref, parent);
    }

    public override Vector2Int GetTilePosition()
    {
        return _model.TileCoords;
    }

    public string GetName()
    {
        return _model.Name;
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

    public override event Action<AbsTileModel> UpdateView;
}
