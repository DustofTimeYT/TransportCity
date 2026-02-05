using AbsTile;
using ProsumerTile;
using System;
using UnityEngine;

public class ProsumerTilePresenter : AbsTilePresenter
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
        throw new NotImplementedException();
    }
    
    public override event Action<AbsTileModel> UpdateView;
}
