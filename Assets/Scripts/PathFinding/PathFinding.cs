using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    private ISurroundingCellsFinder _finder;

    private IGrid _grid;
    private Dictionary<Vector2Int,PathFindingCell> _openedList;
    private Dictionary<Vector2Int,PathFindingCell> _closedList;

    public PathFinding(ISurroundingCellsFinder surroundingCellsFinder, IGrid grid)
    {
        _finder = surroundingCellsFinder;
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

        Debug.Log(currentCell.coordinates);

        if (currentCell.coordinates == end)
        {    
            path = CreatePath(start, currentCell);

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
    /// ћетод изучени€ €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCell">ѕредставление текущей €чейки</param>
    /// <param name="endCellCoordinates"> оординаты (x, y) целевой €чейки</param>

    private void ExplorationCells(PathFindingCell currentCell, Vector2Int endCellCoordinates)
    {
        var aroundCells = _finder.FindSurroundingCells(currentCell.coordinates, _grid, _openedList);

        foreach (PathFindingCell activeCell in aroundCells)
        {
            ExplorationActiveCell(activeCell, currentCell.coordinates, currentCell.pathLength, endCellCoordinates);
        }
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

        if (!_openedList.ContainsKey(activeCell.coordinates))
        {
            int calculateHA = PFCalculator.CalculateHeuristicApproximation(activeCell.coordinates, endCellCoordinates);
            activeCell.SetHeuristicApproximation(calculateHA);
            _openedList.Add(activeCell.coordinates, activeCell);
        }

        activeCell.TrySetPathLenght(currentCellCoordinates, PFCalculator.CalculatePathLenght(currentPathLength, activeCell.movementDifficulty));

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

    private IReadOnlyList<Vector2Int> CreatePath(Vector2Int startCoords, PathFindingCell endCell)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int cell = endCell.coordinates;
        do
        {
            path.Add(cell);
            cell = _closedList[cell].previousCellCoords;
        } 
        while (cell != startCoords);

        path.Add(cell);
        path.Reverse();

        return path;
    }
}
