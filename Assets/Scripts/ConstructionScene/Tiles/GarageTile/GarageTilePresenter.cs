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

    public List<ITransport> GetTransports()
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

    public bool FindTransport(string name, out ITransport iTransport)
    {
        iTransport = null;
        foreach (ITransport transport in GetTransports())
        {
            if (transport.GetName() == name)
            {
                iTransport = transport;
                return true;
            }
        }
        return false;
    }


    public override event Action<AbsTileModel> UpdateView;
}
