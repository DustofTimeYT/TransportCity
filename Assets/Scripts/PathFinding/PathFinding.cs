using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private SurroundingCellsGenerator _surroundingCellsGenerator;

    private Dictionary<Vector2Int, CellPresenter> _grid;
    private Dictionary<Vector2Int,PathFindingCell> _openedList;
    private Dictionary<Vector2Int,PathFindingCell> _closedList;

    private int _pathCost = 10;

    public void Init(SurroundingCellsGenerator surroundingCells, Dictionary<Vector2Int, CellPresenter> grid)
    {
        _surroundingCellsGenerator = surroundingCells;
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
    /// ����� �������� ����� ������ ������� ������
    /// </summary>
    /// <param name="currentCell">������������� ������� ������</param>
    /// <param name="endCellCoordinates">���������� (x, y) ������� ������</param>

    private void ExplorationCells(PathFindingCell currentCell, Vector2Int endCellCoordinates)
    {
        var aroundCells = _surroundingCellsGenerator.FindSurroundingCells(currentCell.coordinates, _grid, _openedList);

        foreach (PathFindingCell activeCell in aroundCells)
        {
            ExplorationActiveCell(activeCell, currentCell.coordinates, currentCell.pathlength, endCellCoordinates);
        }
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
            int calculateHA = PFCalculator.CalculateHeuristicApproximation(activeCell.coordinates, endCellCoordinates);
            activeCell.SetHeuristicApproximation(calculateHA);
            _openedList.Add(activeCell.coordinates, activeCell);
        }

        Debug.Log($"HA {activeCell.heuristicApproximation} PL {activeCell.pathlength}");

        activeCell.TrySetPathLenght(currentCellCoordinates, PFCalculator.CalculatePathLenght(currentPathLength, activeCell.movementDifficulty));

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
