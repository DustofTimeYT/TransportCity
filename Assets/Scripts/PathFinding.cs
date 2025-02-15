using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);

    private int[,] _grid;

    private Dictionary<Vector2Int,PathFindingCell> _openedList;
    private Dictionary<Vector2Int,PathFindingCell> _closedList;

    private int _pathCost = 10;

    private void Awake()
    {
        _grid= new int[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                _grid[x, y] = 0;
            }
        }

        _grid[3, 5] = 1;
        _grid[3, 6] = 1;
        _grid[3, 7] = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PathFind(new Vector2Int(1,6), new Vector2Int(7,6));
        }
    }

    private void PathFind(Vector2Int start, Vector2Int end)
    {
        PathFindingCell currentCell = new PathFindingCell(start, 0); // ������ ������ ������� ����������� ������ // ������������� ������� ������� ���������
        currentCell.TrySetPathLenght(0); // ��������� ����� ���� ��� ��������� ������
        /*
        for (int x = -1 ;x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                if (x == 0 || y == 0) {  continue; }

                if (currentCell.coordinates.x + x < 0 || currentCell.coordinates.y + y < 0 ||
                    currentCell.coordinates.x + x > gridSize.x || currentCell.coordinates.y + y < gridSize.y)
                {
                    continue;
                }

                PathFindingCell activeCell = new PathFindingCell();

                activeCell.SetHeuristicApproximation(CalculateHeuristicApproximation(activeCell.coordinates, end));

            }
        }

        for (int y = -1; y < 1; y++)
        {
            if (y == 0) { continue; }

            Vector2Int activeCellCoordinates = new(currentCell.coordinates.x, currentCell.coordinates.y + y );

            if (activeCellCoordinates.y < 0 || activeCellCoordinates.y < gridSize.y)
            {
                continue;
            }
            
            PathFindingCell activeCell;
            
            if (_openedList.TryGetValue(activeCellCoordinates, out activeCell))
            {
                activeCell.TrySetPathLenght(CalculatePathLenght(currentCell.pathlength));
            }
            else
            {
                activeCell = new PathFindingCell();
                activeCell.SetHeuristicApproximation(CalculateHeuristicApproximation(activeCell.coordinates, end));
                activeCell.TrySetPathLenght(CalculatePathLenght(currentCell.pathlength));
            }

            activeCell.CalculateCellWeight();

        }
        */
        ExplorationCells(currentCell, end);
        _closedList.Add(currentCell.coordinates, currentCell);
        _openedList.Remove(currentCell.coordinates);
        currentCell = SelectNewCurrentCell();
    }

    /// <summary>
    /// ����� �������� �������������� ����������� � ������� �������������� ���������� 
    /// </summary>
    /// <param name="currentCell">���������� (x, y) ������ ��� ������� �������������� �����������</param>
    /// <param name="endCell">���������� (x, y) ������� ������</param>
    /// <returns></returns>

    private int CalculateHeuristicApproximation(Vector2Int currentCell, Vector2Int endCell)
    {
        return Mathf.Abs(currentCell.x - endCell.x) + Mathf.Abs(currentCell.y - endCell.y);
    }

    /// <summary>
    /// ����� �������� ����� ���� �� ��������� ������
    /// </summary>
    /// <param name="currentCellPathLenght"> ����� ���� �� �������������� ������</param>
    /// <returns></returns>

    private int CalculatePathLenght(int currentCellPathLenght)
    {
        return currentCellPathLenght + _pathCost;
    }

    /// <summary>
    /// ����� �������� ����� ������ ������� ������
    /// </summary>
    /// <param name="currentCell"> ������������� ������� ������</param>
    /// <param name="endCellCoordinates">���������� (x, y) ������� ������</param>

    private void ExplorationCells(PathFindingCell currentCell, Vector2Int endCellCoordinates)
    {
        List<Vector2Int> aroundCellsCoordinates = FindAroundCells(currentCell.coordinates);
        foreach (Vector2Int activeCellCoordinates in aroundCellsCoordinates)
        {
            ExplorationActiveCell(activeCellCoordinates, currentCell.pathlength, endCellCoordinates);
        }
    }

    /// <summary>
    /// ����� ������ ����� ������ ������� ������
    /// </summary>
    /// <param name="currentCellCoordinates">���������� (x, y) ������� ������ </param>
    /// <returns></returns>

    private List<Vector2Int> FindAroundCells(Vector2Int currentCellCoordinates)
    {
        List<Vector2Int> cells = new List<Vector2Int>();
        for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                if (x==0  && y!=0 || x!=0 && y == 0)
                {
                    if (currentCellCoordinates.x - x < 0 || currentCellCoordinates.x + x < gridSize.x)
                    {
                        continue;
                    }
                    if (currentCellCoordinates.y - y < 0 || currentCellCoordinates.y + y < gridSize.y)
                    {
                        continue;
                    }
                    Vector2Int cell = new(currentCellCoordinates.x - x, currentCellCoordinates.y - y);
                    cells.Add(cell);
                }
            }
        }
        return cells;
    }

    /// <summary>
    /// ����� ������������ �������� ������, ������� ��������� ������ ������� ������
    /// </summary>
    /// <param name="activeCellCoordinates"> ���������� (x, y) �������� ������</param>
    /// <param name="currentPathLength"> ����� ���� ������� ������, ������� ���������� ��� ���������� ������� ������ �� ���������</param>
    /// <param name="endCellCoordinates"> ���������� (x, y) ������� ������</param>

    private void ExplorationActiveCell(Vector2Int activeCellCoordinates, int currentPathLength, Vector2Int endCellCoordinates)
    { 
        if (_closedList.ContainsKey(activeCellCoordinates)) { return; }

        PathFindingCell activeCell;

        if (_openedList.TryGetValue(activeCellCoordinates, out activeCell))
        {
            activeCell.TrySetPathLenght(CalculatePathLenght(currentPathLength));
        }
        else
        {
            int currentHeuristicApproximation = CalculateHeuristicApproximation(activeCell.coordinates, endCellCoordinates);
            activeCell = new PathFindingCell(activeCellCoordinates, currentHeuristicApproximation);
            activeCell.TrySetPathLenght(CalculatePathLenght(currentPathLength));
            _openedList.Add(activeCellCoordinates, activeCell);
        }

        activeCell.CalculateCellWeight();
    }

    /// <summary>
    /// ����� ������ ��������� ������� ������ 
    /// </summary>
    /// <returns>������ � ����������� ��������� </returns>

    private PathFindingCell SelectNewCurrentCell()
    {
        PathFindingCell cellWithMinPathLenght;
        List<PathFindingCell> openedList;
        openedList = _openedList.Values.ToList();

        cellWithMinPathLenght = openedList[0];

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

        return cellWithMinPathLenght;
    }
}
