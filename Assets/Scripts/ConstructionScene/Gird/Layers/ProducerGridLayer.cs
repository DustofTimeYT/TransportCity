using Grid;
using System.Collections.Generic;
using UnityEngine;

public class ProducerGridLayer : IProducerGrid
{
    private GridLayer<IProducer> _gridLayer;

    public ProducerGridLayer(GridPresenter grid)
    {
        _gridLayer = new(grid);
        Debug.Log($"{this} was created");
    }

    public Dictionary<Vector2Int, IProducer> GetProducers()
    {
        return _gridLayer.GetTiles();
    }

    public bool TryGetTile(Vector2Int tilePos, out IProducer tile)
    {
        return _gridLayer.TryGetTile(tilePos, out tile);
    }
}
