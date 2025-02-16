using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

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

        _grid[3, 3] = 1;
        _grid[3, 4] = 1;
        _grid[3, 5] = 1;
        _grid[3, 6] = 1;
        _grid[3, 7] = 1;

        _openedList = new Dictionary<Vector2Int, PathFindingCell>();
        _closedList = new Dictionary<Vector2Int, PathFindingCell>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PathFind(new Vector2Int(1,6), new Vector2Int(7,5));
        }
    }

    private void PathFind(Vector2Int start, Vector2Int end)
    {
        PathFindingCell currentCell = new PathFindingCell(start, start , 0, 0); // €чейка вокруг которой исследуютс€ €чейки // устанавливаем стартовую €чейку текущей и обнул€ем значени€
        _openedList.Add(currentCell.coordinates, currentCell);
        Debug.Log(_openedList.Count);

        while (_openedList.Count == 0 || currentCell.coordinates != end)
        {
            ExplorationCells(currentCell, end);
            _closedList.Add(currentCell.coordinates, currentCell);
            _openedList.Remove(currentCell.coordinates);
            currentCell = SelectNewCurrentCell();
        }

        if (_openedList.Count == 0)
        {
            Debug.Log($"ѕуть от €чейки {start} до €чейки {end} не найден");
            return;
        }
        else
        {
            Debug.Log("Path");
            var path = CreatePath(start, currentCell);
            foreach (var pathCell in path) 
            {
                Debug.Log(pathCell);
            }
        }
    }

    /// <summary>
    /// ћетод рассчета эвристического приближени€ с помощью манхэттенского рассто€ни€ 
    /// </summary>
    /// <param name="currentCell"> оординаты (x, y) €чейки дл€ которой рассчитываетс€ приближение</param>
    /// <param name="endCell"> оординаты (x, y) целевой €чейки</param>
    /// <returns></returns>

    private int CalculateHeuristicApproximation(Vector2Int currentCell, Vector2Int endCell)
    {
        return (Mathf.Abs(currentCell.x - endCell.x) + Mathf.Abs(currentCell.y - endCell.y)) * _pathCost;
    }

    /// <summary>
    /// ћетод рассчета длины пути до выбранной клетки
    /// </summary>
    /// <param name="currentCellPathLenght"> ƒлина пути до предшествующей клетки</param>
    /// <returns></returns>

    private int CalculatePathLenght(int currentCellPathLenght)
    {
        return currentCellPathLenght + _pathCost;
    }

    /// <summary>
    /// ћетод изучени€ €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCell"> представление текущей €чейки</param>
    /// <param name="endCellCoordinates"> оординаты (x, y) целевой €чейки</param>

    private void ExplorationCells(PathFindingCell currentCell, Vector2Int endCellCoordinates)
    {
        var aroundCellsCoordinates = FindAvailableSurroundingCells(currentCell.coordinates);

        foreach (Vector2Int activeCellCoordinates in aroundCellsCoordinates)
        {
            ExplorationActiveCell(activeCellCoordinates, currentCell.coordinates, currentCell.pathlength, endCellCoordinates);
        }
    }

    /// <summary>
    /// ћетод поиска €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCellCoordinates"> оординаты (x, y) текущей €чейки </param>
    /// <returns></returns>
    
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

                    if (cell.x < 0 || cell.x > gridSize.x)
                    {
                        continue;
                    }
                    if (cell.y < 0 || cell.y > gridSize.y)
                    {
                        continue;
                    }
                    if (_grid[cell.x, cell.y] == 1)
                    {
                        continue;
                    }
                    surroundingCells.Add(cell);
                }
            }
        }
        return surroundingCells;
    }

    /// <summary>
    /// ћетод исследовани€ активной €чейки, котора€ находитс€ вокруг текущей €чейки
    /// </summary>
    /// <param name="activeCellCoordinates">  оординаты (x, y) активной €чейки</param>
    /// <param name="currentPathLength"> ƒлина пути текущей €чейки, котора€ необходима дл€ достижени€ текущей €чейки из стартовой</param>
    /// <param name="endCellCoordinates">  оординаты (x, y) целевой €чейки</param>

    private void ExplorationActiveCell(Vector2Int activeCellCoordinates, Vector2Int currentCellCoordinates, int currentPathLength, Vector2Int endCellCoordinates)
    { 
        if (_closedList.ContainsKey(activeCellCoordinates)) { return; }

        PathFindingCell activeCell;

        Debug.Log($"currentCellCoordinates {currentCellCoordinates}");

        if (_openedList.TryGetValue(activeCellCoordinates, out activeCell))
        {
            activeCell.TrySetPathLenght(currentCellCoordinates, CalculatePathLenght(currentPathLength));
        }
        else
        {
            int calculateHA = CalculateHeuristicApproximation(activeCellCoordinates, endCellCoordinates);
            int calculatePL = CalculatePathLenght(currentPathLength);
            activeCell = new PathFindingCell(activeCellCoordinates, currentCellCoordinates, calculatePL, calculateHA);
            _openedList.Add(activeCellCoordinates, activeCell);
        }

        Debug.Log($"Cell {activeCell.coordinates}");
        Debug.Log($"PathLenght {activeCell.pathlength}");
        Debug.Log($"HeuristicApproximation {activeCell.heuristicApproximation}");
        Debug.Log($"previousCell {activeCell.previousCell}");

        activeCell.CalculateCellWeight();
    }

    /// <summary>
    /// ћетод выбора следующей текущей €чейки 
    /// </summary>
    /// <returns>ячейку с минимальным значением </returns>

    private PathFindingCell SelectNewCurrentCell()
    {
        PathFindingCell cellWithMinPathLenght;
        List<PathFindingCell> openedList;
        
        openedList = _openedList.Values.ToList();

        cellWithMinPathLenght = openedList[0];

        foreach (PathFindingCell currentCell in openedList)
        {
            // ≈сли вес текущей €чейки больше, чем найденый минимальный вес €чейки, то сохран€ем текущую €чейку
            if (currentCell.cellWeight < cellWithMinPathLenght.cellWeight)
            { 
                cellWithMinPathLenght = currentCell; 
            }
            else if (currentCell.cellWeight == cellWithMinPathLenght.cellWeight)
            {
                // ≈сли у них одинаковый вес, то ищем минимальное эвристическое приближение
                if (currentCell.heuristicApproximation < cellWithMinPathLenght.heuristicApproximation)
                {
                    cellWithMinPathLenght = currentCell;
                }
                else if (currentCell.heuristicApproximation == cellWithMinPathLenght.heuristicApproximation)
                {
                    // ≈сли у них одинаковое эвристическое приближение, то €чейка выбираетс€ слуайным образом
                    int rand = Random.Range(0, 100);
                    if (rand < 50)
                    {
                        cellWithMinPathLenght = currentCell;
                    }
                }
            }
        }

        Debug.Log("|||||||||");
        Debug.Log(cellWithMinPathLenght.coordinates);
        Debug.Log("|||||||||");

        return cellWithMinPathLenght;
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
