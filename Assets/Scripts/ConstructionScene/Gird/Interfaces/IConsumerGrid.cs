using System.Collections.Generic;
using UnityEngine;

public interface IConsumerGrid
{
    public bool TryGetTile(Vector2Int tilePos, out IConsumer tile);

    public Dictionary<Vector2Int, IConsumer> GetConsumers();
}