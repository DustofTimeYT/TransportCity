using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ISurroundingCellsFinder
{
    public IReadOnlyList<PathFindingCell> FindSurroundingCells(Vector2Int currentCellCoordinates, IGrid grid, Dictionary<Vector2Int, PathFindingCell> openedList);
}
