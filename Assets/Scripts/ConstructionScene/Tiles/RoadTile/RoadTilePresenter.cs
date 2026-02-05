using System;
using AbsTile;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

public class RoadTilePresenter : AbsTilePresenter, IMovable
{
    readonly private RoadTileModel _model;

    public RoadTilePresenter(Vector2Int coords, AbsTileConfig config, Transform parent)
    {
        if (config.TileType != TileType.Road) return;
        _model = new RoadTileModel(config, coords);
        InstantiateView(config.TilePref, parent);
    }

    public int GetMovementDifficulty()
    {
        return _model.MovementDifficulty;
    }

    public void SetMovementDifficulty(int value)
    {
        _model.TrySetMovementDifficulty(value);
        UpdateView?.Invoke(_model);
    }

    public override Vector2Int GetTilePosition()
    {
        return _model.TileCoords;
    }

    public override event Action<AbsTileModel> UpdateView;
}
