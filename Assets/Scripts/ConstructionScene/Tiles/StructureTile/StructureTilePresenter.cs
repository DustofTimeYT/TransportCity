using System;
using StructureTile;
using UnityEngine;
using AbsTile;

public class StructureTilePresenter : AbsTilePresenter
{
    readonly private StructureTileModel _model;

    public StructureTilePresenter(Vector2Int coords, AbsTileConfig config, Transform parent)
    {
        if (config.TileType != TileType.Structure) return;
        _model = new StructureTileModel(config, coords);
        InstantiateView(config.TilePref, parent);
    }

    public override event Action<AbsTileModel> UpdateView;

    public override Vector2Int GetTilePosition()
    {
        return _model.TileCoords;
    }
}
