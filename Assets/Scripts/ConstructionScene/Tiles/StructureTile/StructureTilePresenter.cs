using StructureTile;
using UnityEngine;
using AbsTile;

public class StructureTilePresenter : AbsTilePresenter<StructureTileModel>
{
    public StructureTilePresenter(Vector2Int coords, int rotationAngle, AbsTileConfig config, Transform parent) : base(coords, rotationAngle, config, parent)
    {
        if (config.TileType != TileType.Structure) return;

        _model = new StructureTileModel(config, rotationAngle, coords);

        InstantiateView(config.TilePref, rotationAngle, parent);
    }
}
