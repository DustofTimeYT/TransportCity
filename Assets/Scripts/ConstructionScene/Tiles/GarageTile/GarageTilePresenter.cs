using AbsMoveTile;
using GarageTile;
using System.Collections.Generic;
using UnityEngine;

public class GarageTilePresenter : AbsMoveTilePresenter<GarageTileModel>, IGarage
{
    public GarageTilePresenter(Vector2Int coords, int rotationAngle, AbsTileConfig config, Transform parent) : base(coords, rotationAngle, config, parent)
    {
        if (config.TileType != TileType.Garage) return;

        _model = new GarageTileModel(config, rotationAngle, coords);

        InstantiateView(config.TilePref, rotationAngle, parent);
    }

    public List<ITransport> GetTransports()
    {
        return _model.Transports;
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
}
