
using System.Collections.Generic;
using UnityEngine;

public class SurroundingCellsFinder: ISurroundingCellsFinder
{
    /// <summary>
    /// ћетод поиска €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCellCoordinates"> оординаты (x, y) текущей €чейки</param>
    /// <param name="grid">—писок €чеек, отображаемых пользователю</param>
    /// <param name="openedList">—писок €чеек, которые были обнаружены на прошлом этапе исследовани€<param>
    /// <returns>—писок клеток наход€щихс€ вокруг переданной клетки в форме +</returns>

    public IReadOnlyList<PathFindingCell> FindSurroundingCells(Vector2Int currentCellCoordinates, IGrid grid, Dictionary<Vector2Int, PathFindingCell> openedList)
    {
        List<PathFindingCell> surroundingCells = new List<PathFindingCell>();
        Vector2Int cell;
        for (int i = -1; i <= 1; i+=2)
        {
            cell = new(currentCellCoordinates.x + i, currentCellCoordinates.y);
            CheckCell(cell, surroundingCells, grid, openedList);

            cell = new(currentCellCoordinates.x, currentCellCoordinates.y + i);
            CheckCell(cell, surroundingCells, grid, openedList);
        }
        return surroundingCells;
    }

    private void CheckCell(Vector2Int cell, List<PathFindingCell> surroundingCells, IGrid grid, Dictionary<Vector2Int, PathFindingCell> openedList)
    {
        if (openedList.TryGetValue(new Vector2Int(cell.x, cell.y), out PathFindingCell PFCell))
        {
            surroundingCells.Add(PFCell);
        }

        if (grid.TryGetCell(new Vector2Int(cell.x, cell.y), out IMoveable cellData))
        {
                surroundingCells.Add(new PathFindingCell(cell, cellData.GetMovementDifficulty()));
            if (cellData.GetCellStateType() != CellStateType.UnrichmentCell)
            {
            }          
        }
    }
}
