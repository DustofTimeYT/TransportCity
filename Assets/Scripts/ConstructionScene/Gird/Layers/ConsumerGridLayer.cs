using Grid;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerGridLayer : IConsumerGrid
{
    private GridLayer<IConsumer> _gridLayer;

    public ConsumerGridLayer(GridPresenter grid)
    {
        _gridLayer = new(grid);
        Debug.Log($"{this} was created");
    }

    public Dictionary<Vector2Int, IConsumer> GetConsumers()
    {
        return _gridLayer.GetTiles();
    }

    public bool TryGetTile(Vector2Int tilePos, out IConsumer tile)
    {
        return _gridLayer.TryGetTile(tilePos, out tile);
    }
}
