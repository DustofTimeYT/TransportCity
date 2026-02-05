using System.Collections.Generic;
using UnityEngine;
using PathFindAlgo;

public interface IPathFinder
{
    public bool TryPathFind(Vector2Int start, Vector2Int end, out IReadOnlyList<Vector2Int> path);
}
