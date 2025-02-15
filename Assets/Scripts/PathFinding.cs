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
        PathFindingCell currentCell = new PathFindingCell(start, 0); // €чейка вокруг которой исследуютс€ €чейки // устанавливаем текущей €чейкой стартовую
        currentCell.TrySetPathLenght(0); // обнуление длины пути дл€ начальной €чейки
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
    /// ћетод рассчета эвристического приближени€ с помощью манхэттенского рассто€ни€ 
    /// </summary>
    /// <param name="currentCell"> оординаты (x, y) €чейки дл€ которой рассчитываетс€ приближение</param>
    /// <param name="endCell"> оординаты (x, y) целевой €чейки</param>
    /// <returns></returns>

    private int CalculateHeuristicApproximation(Vector2Int currentCell, Vector2Int endCell)
    {
        return Mathf.Abs(currentCell.x - endCell.x) + Mathf.Abs(currentCell.y - endCell.y);
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
        List<Vector2Int> aroundCellsCoordinates = FindAroundCells(currentCell.coordinates);
        foreach (Vector2Int activeCellCoordinates in aroundCellsCoordinates)
        {
            ExplorationActiveCell(activeCellCoordinates, currentCell.pathlength, endCellCoordinates);
        }
    }

    /// <summary>
    /// ћетод поиска €чеек вокруг текущей €чейки
    /// </summary>
    /// <param name="currentCellCoordinates"> оординаты (x, y) текущей €чейки </param>
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
    /// ћетод исследовани€ активной €чейки, котора€ находитс€ вокруг текущей €чейки
    /// </summary>
    /// <param name="activeCellCoordinates">  оординаты (x, y) активной €чейки</param>
    /// <param name="currentPathLength"> ƒлина пути текущей €чейки, котора€ необходима дл€ достижени€ текущей €чейки из стартовой</param>
    /// <param name="endCellCoordinates">  оординаты (x, y) целевой €чейки</param>

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

        return cellWithMinPathLenght;
    }
}
