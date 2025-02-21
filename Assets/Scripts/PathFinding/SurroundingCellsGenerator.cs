
using System.Collections.Generic;
using UnityEngine;

public class SurroundingCellsGenerator
{

    private Vector2Int _gridSize;

    public SurroundingCellsGenerator(GridConfig gridConfig)
    {
        _gridSize = gridConfig.GridSize;
    }

    /// <summary>
    /// ����� ������ ����� ������ ������� ������
    /// </summary>
    /// <param name="currentCellCoordinates">���������� (x, y) ������� ������</param>
    /// <param name="grid">������ �����, ������������ ������������</param>
    /// <param name="openedList">������ �����, ������� ���� ���������� �� ������� ����� ������������<param>
    /// <returns>������ ������ ����������� ������ ���������� ������ � ����� +</returns>

    public IReadOnlyList<PathFindingCell> FindSurroundingCells(Vector2Int currentCellCoordinates, Dictionary<Vector2Int, CellPresenter>  grid, Dictionary<Vector2Int, PathFindingCell> openedList)
    {
        List<PathFindingCell> surroundingCells = new List<PathFindingCell>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y != 0 || x != 0 && y == 0)
                {
                    Vector2Int cell = new(currentCellCoordinates.x + x, currentCellCoordinates.y + y);

                    if (openedList.TryGetValue(new Vector2Int(cell.x, cell.y), out PathFindingCell PFCell))
                    {
                        surroundingCells.Add(PFCell);
                        continue;
                    }

                    if (grid.TryGetValue(new Vector2Int(cell.x, cell.y), out CellPresenter cellData))
                    {
                        if (cellData.GetCellStateType() == CellStateType.UnrichmentCell)
                        {
                            continue;
                        }
                        surroundingCells.Add(new PathFindingCell(cell, cellData.GetMovementDifficulty()));
                    }
                }
            }
        }
        return surroundingCells;
    }
}
