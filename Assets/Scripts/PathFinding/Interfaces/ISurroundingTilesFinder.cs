using System.Collections.Generic;
using UnityEngine;
using PathFindAlgo;

public interface ISurroundingTilesFinder
{
    public IReadOnlyList<PathFindingTile> FindSurroundingTiles(Vector2Int currentTileCoordinates, IMoveGrid grid, Dictionary<Vector2Int, PathFindingTile> openedList);
}
