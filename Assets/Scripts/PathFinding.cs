using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class PathFinding : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private Dictionary<Vector2Int, CellPresenter> _grid;
    private Dictionary<Vector2Int,PathFindingCell> _openedList;
    private Dictionary<Vector2Int,PathFindingCell> _closedList;

    private int _pathCost = 10;

    public void Init(Dictionary<Vector2Int, CellPresenter> grid)
    {
        _grid = grid;
        _openedList = new Dictionary<Vector2Int, PathFindingCell>();
        _closedList = new Dictionary<Vector2Int, PathFindingCell>();
    }

    public bool TryPathFind(Vector2Int start, Vector2Int end, out IReadOnlyList<Vector2Int> path)
    {
        _openedList.Clear();
        _closedList.Clear();
        path = null;

        if(start == null || end == null) return false;

        PathFindingCell currentCell = new PathFindingCell(start, 0 ); // ������ ������ ������� ����������� ������ // ������������� ��������� ������ ������� � �������� ��������
        _openedList.Add(currentCell.coordinates, currentCell);
        Debug.Log("Start: " + start);
        Debug.Log("End: " + end);

        while (_openedList.Count != 0)
        {
            if (TrySelectNewCurrentCell(out currentCell))
            {
                ExplorationCells(currentCell, end);
                _closedList.Add(currentCell.coordinates, currentCell);
                _openedList.Remove(currentCell.coordinates);
            }

            if (currentCell.coordinates == end) { break; }
        }

        if (currentCell.coordinates == end)
        {    
            Debug.Log("Path");
            path = CreatePath(start, currentCell);
            foreach (var pathCell in path)
            {
                Debug.Log(pathCell);
            }
            _closedList.Clear();
            _openedList.Clear();
        }
        else
        {
            Debug.Log($"���� �� ������ {start} �� ������ {end} �� ������");
            _closedList.Clear();
            _openedList.Clear();
        }

        return true;
    }

    /// <summary>
    /// ����� �������� �������������� ����������� � ������� �������������� ���������� 
    /// </summary>
    /// <param name="currentCell">���������� (x, y) ������ ��� ������� �������������� �����������</param>
    /// <param name="endCell">���������� (x, y) ������� ������</param>
    /// <returns></returns>

    private int CalculateHeuristicApproximation(Vector2Int currentCell, Vector2Int endCell)
    {
        return (Mathf.Abs(currentCell.x - endCell.x) + Mathf.Abs(currentCell.y - endCell.y)) * _pathCost;
    }

    /// <summary>
    /// ����� �������� ����� ���� �� ��������� ������
    /// </summary>
    /// <param name="currentCellPathLenght"> ����� ���� �� �������������� ������</param>
    /// <returns>����� ���� �� ���������� ������</returns>

    private int CalculatePathLenght(int currentCellPathLenght, int movementDifficulty)
    {
        return currentCellPathLenght + _pathCost * movementDifficulty;
    }

    /// <summary>
    /// ����� �������� ����� ������ ������� ������
    /// </summary>
    /// <param name="currentCell"> ������������� ������� ������</param>
    /// <param name="endCellCoordinates">���������� (x, y) ������� ������</param>

    private void ExplorationCells(PathFindingCell currentCell, Vector2Int endCellCoordinates)
    {
        var aroundCellsCoordinates = FindAvailableSurroundingCells(currentCell.coordinates);

        foreach (PathFindingCell activeCellCoordinates in aroundCellsCoordinates)
        {
            ExplorationActiveCell(activeCellCoordinates, currentCell.coordinates, currentCell.pathlength, endCellCoordinates);
        }
    }

    /// <summary>
    /// ����� ������ ����� ������ ������� ������
    /// </summary>
    /// <param name="currentCellCoordinates">���������� (x, y) ������� ������ </param>
    /// <returns></returns>
    /*
    private IReadOnlyList<Vector2Int> FindAvailableSurroundingCells(Vector2Int currentCellCoordinates)
    {
        List<Vector2Int> surroundingCells = new List<Vector2Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x==0  && y!=0 || x!=0 && y == 0)
                {
                    Vector2Int cell = new(currentCellCoordinates.x + x, currentCellCoordinates.y + y);

                    if (cell.x < 0 || cell.x >= gridSize.x)
                    {
                        continue;
                    }
                    if (cell.y < 0 || cell.y >= gridSize.y)
                    {
                        continue;
                    }
                    if (_grid.TryGetValue(new Vector2Int(cell.x, cell.y), out CellPresenter cellData))
                    {
                        if (cellData.GetCellStateType() == CellStateType.UnrichmentCell )
                        {
                            continue;
                        }
                    }
  
                    surroundingCells.Add(cell);
                }
            }
        }
        return surroundingCells;
    }
    */
    private IReadOnlyList<PathFindingCell> FindAvailableSurroundingCells(Vector2Int currentCellCoordinates)
    {
        List<PathFindingCell> surroundingCells = new List<PathFindingCell>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y != 0 || x != 0 && y == 0)
                {
                    Vector2Int cell = new(currentCellCoordinates.x + x, currentCellCoordinates.y + y);

                    if (cell.x < 0 || cell.x >= gridSize.x)
                    {
                        continue;
                    }
                    if (cell.y < 0 || cell.y >= gridSize.y)
                    {
                        continue;
                    }
                    if (_grid.TryGetValue(new Vector2Int(cell.x, cell.y), out CellPresenter cellData))
                    {
                        if (cellData.GetCellStateType() == CellStateType.UnrichmentCell)
                        {
                            continue;
                        }
                    }

                    surroundingCells.Add(new PathFindingCell(cell, cellData.GetMovementDifficulty()));
                }
            }
        }
        return surroundingCells;
    }

    /// <summary>
    /// ����� ������������ �������� ������, ������� ��������� ������ ������� ������
    /// </summary>
    /// <param name="activeCell"> ���������� (x, y) �������� ������</param>
    /// <param name="currentPathLength"> ����� ���� ������� ������, ������� ���������� ��� ���������� ������� ������ �� ���������</param>
    /// <param name="endCellCoordinates"> ���������� (x, y) ������� ������</param>

    private void ExplorationActiveCell(PathFindingCell activeCell, Vector2Int currentCellCoordinates, int currentPathLength, Vector2Int endCellCoordinates)
    { 
        if (_closedList.ContainsKey(activeCell.coordinates)) { return; }

        Debug.Log(activeCell.coordinates);

        if (!_openedList.ContainsKey(activeCell.coordinates))
        {
            int calculateHA = CalculateHeuristicApproximation(activeCell.coordinates, endCellCoordinates);
            activeCell.SetHeuristicApproximation(calculateHA);
            _openedList.Add(activeCell.coordinates, activeCell);
        }

        activeCell.TrySetPathLenght(currentCellCoordinates, CalculatePathLenght(currentPathLength, activeCell.movementDifficulty));

        activeCell.CalculateCellWeight();
    }

    /// <summary>
    /// ����� ������ ��������� ������� ������ 
    /// </summary>
    /// <returns>������ � ����������� ��������� </returns>

    private bool TrySelectNewCurrentCell(out PathFindingCell cellWithMinPathLenght)
    {
        List<PathFindingCell> openedList;
        
        openedList = _openedList.Values.ToList();

        if (openedList.Count > 0)
        {
            cellWithMinPathLenght = openedList[0];
            openedList.RemoveAt(0);

            foreach (PathFindingCell currentCell in openedList)
            {
                // ���� ��� ������� ������ ������, ��� �������� ����������� ��� ������, �� ��������� ������� ������
                if (currentCell.cellWeight < cellWithMinPathLenght.cellWeight)
                {
                    cellWithMinPathLenght = currentCell;
                }
                else if (currentCell.cellWeight == cellWithMinPathLenght.cellWeight)
                {
                    // ���� � ��� ���������� ���, �� ���� ����������� ������������� �����������
                    if (currentCell.heuristicApproximation < cellWithMinPathLenght.heuristicApproximation)
                    {
                        cellWithMinPathLenght = currentCell;
                    }
                    else if (currentCell.heuristicApproximation == cellWithMinPathLenght.heuristicApproximation)
                    {
                        // ���� � ��� ���������� ������������� �����������, �� ������ ���������� �������� �������
                        int rand = Random.Range(0, 100);
                        if (rand < 50)
                        {
                            cellWithMinPathLenght = currentCell;
                        }
                    }
                }
            }
        }
        else
        {
            cellWithMinPathLenght = null;
            return false;
        }
        return true;
    }

    private IReadOnlyList<Vector2Int> CreatePath(Vector2Int startCell, PathFindingCell endCell)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        path.Add(endCell.coordinates);
        Vector2Int cell = endCell.previousCell;

        while (cell != startCell)
        {
            path.Add(cell);
            cell = _closedList[cell].previousCell;
        }

        path.Add(cell);
        path.Reverse();

        return path;
    }
}
