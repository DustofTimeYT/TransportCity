using System.Collections.Generic;
using UnityEngine;

public interface IProducerGrid
{
    public bool TryGetTile(Vector2Int tilePos, out IProducer tile);

    public Dictionary<Vector2Int, IProducer> GetProducers();
}