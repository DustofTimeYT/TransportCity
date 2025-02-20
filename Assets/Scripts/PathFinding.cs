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

        PathFindingCell currentCell = new PathFindingCell(start, 0 ); // €чейка вокруг которой исследуютс€ €чейки // устанавливаем стартовую €чейку текущей и обнул€ем значени€
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
            Debug.Log($"ѕуть от €чейки {start} до €чейки {end} не найден");
            _closedList.Clear();
            _openedList.Clear();
        }

        return true;
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
    /// <returns>длину пути до переданной клетки</returns>

    private int CalculatePathLenght(int currentCellPathLenght, int movementDifficulty)
    {
        return currentCellPathLenght + _pathCost * movementDifficulty;
    }

    /// <summary>
    /// ћетод изучени€ €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCell"> представление текущей €чейки</param>
    /// <param name="endCellCoordinates"> оординаты (x, y) целевой €чейки</param>

    private void ExplorationCells(PathFindingCell currentCell, Vector2Int endCellCoordinates)
    {
        var aroundCellsCoordinates = FindAvailableSurroundingCells(currentCell.coordinates);

        foreach (PathFindingCell activeCellCoordinates in aroundCellsCoordinates)
        {
            ExplorationActiveCell(activeCellCoordinates, currentCell.coordinates, currentCell.pathlength, endCellCoordinates);
        }
    }

    /// <summary>
    /// ћетод поиска €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCellCoordinates"> оординаты (x, y) текущей €чейки </param>
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
    /// ћетод исследовани€ активной €чейки, котора€ находитс€ вокруг текущей €чейки
    /// </summary>
    /// <param name="activeCell">  оординаты (x, y) активной €чейки</param>
    /// <param name="currentPathLength"> ƒлина пути текущей €чейки, котора€ необходима дл€ достижени€ текущей €чейки из стартовой</param>
    /// <param name="endCellCoordinates">  оординаты (x, y) целевой €чейки</param>

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
    /// ћетод выбора следующей текущей €чейки 
    /// </summary>
    /// <returns>ячейку с минимальным значением </returns>

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
