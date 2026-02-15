using AbsTile;
using GarageTile;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GarageTilePresenter : AbsTilePresenter, IMovable, IGarage
{
    GarageTileModel _model;

    public GarageTilePresenter(Vector2Int coords, AbsTileConfig config, Transform parent)
    {
        if (config.TileType != TileType.Garage) return;
        _model = new GarageTileModel(config, coords);
        InstantiateView(config.TilePref, parent);
    }

    public override Vector2Int GetTilePosition()
    {
        return _model.TileCoords;
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

    List<ITransport> IGarage.GetTransports()
    {
        return _model.Transports;
    }

    public string GetName()
    {
        return _model.Name;
    }

    public void AddTransport(ITransport transport)
    {
        _model.Transports.Add(transport);
    }

    public override event Action<AbsTileModel> UpdateView;
}
