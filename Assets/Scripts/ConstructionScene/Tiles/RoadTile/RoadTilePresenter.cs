using AbsMoveTile;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

public class RoadTilePresenter : AbsMoveTilePresenter<RoadTileModel>
{
    public RoadTilePresenter(Vector2Int coords, int rotationAngle, AbsTileConfig config, Transform parent) : base(coords, rotationAngle, config, parent)
    {
        if (config.TileType != TileType.Road) return;

        _model = new RoadTileModel(config as AbsMoveTileConfig, rotationAngle, coords);

        InstantiateView(config.TilePref, rotationAngle, parent);
    }
}
