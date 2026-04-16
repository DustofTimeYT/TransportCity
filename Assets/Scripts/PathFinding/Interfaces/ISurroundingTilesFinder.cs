using System.Collections.Generic;
using UnityEngine;
using PathFindAlgo;

public interface ISurroundingTilesFinder
{
    public IReadOnlyList<PathFindingTile> FindSurroundingTiles(Vector2Int currentTileCoordinates, Dictionary<Vector2Int, PathFindingTile> openedList);

    public PathFindingTile FindCurrentTile(Vector2Int currentTileCoordinates);

    public void SetGrid(IMoveGrid grid);
}
