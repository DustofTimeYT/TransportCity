using UnityEngine;

public class PFCalculator
{
    private static int _pathCost = 10;

    /// <summary>
    /// Метод рассчета длины пути до выбранной клетки
    /// </summary>
    /// <param name="currentCellPathLenght">Длина пути до предшествующей клетки</param>
    /// <param name="movementDifficulty">Сложность перемещения по текущей клетке</param>
    /// <returns>Длину пути до переданной клетки</returns>

    public static int CalculatePathLenght(int currentCellPathLenght, int movementDifficulty)
    {
        return currentCellPathLenght + _pathCost * movementDifficulty;
    }

    /// <summary>
    /// Метод рассчета эвристического приближения с помощью манхэттенского расстояния 
    /// </summary>
    /// <param name="currentCell">Координаты (x, y) ячейки для которой рассчитывается приближение</param>
    /// <param name="endCell">Координаты (x, y) целевой ячейки</param>
    /// <returns>Эвристическое приближение текущей клетки</returns>

    public static int CalculateHeuristicApproximation(Vector2Int currentCell, Vector2Int endCell)
    {
        return (Mathf.Abs(currentCell.x - endCell.x) + Mathf.Abs(currentCell.y - endCell.y)) * _pathCost;
    }
}
