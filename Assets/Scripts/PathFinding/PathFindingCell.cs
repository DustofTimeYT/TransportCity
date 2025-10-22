using System;
using UnityEngine;

public class PathFindingCell
{
    public PathFindingCell(Vector2Int coordinates, int movementDifficulty)
    {
        this.coordinates = coordinates;
        this.movementDifficulty = movementDifficulty;
        pathLength = 0;
    }
    public int movementDifficulty { get; private set; }

    public Vector2Int coordinates {  get; private set; }

    public Vector2Int previousCellCoords { get; private set; }
    public int cellWeight { get; private set; }
    public int pathLength { get; private set; }
    public int heuristicApproximation { get; private set; }

    /// <summary>
    /// Метод рассчета веса ячейки
    /// </summary>

    public void CalculateCellWeight()
    {
        cellWeight = pathLength + heuristicApproximation;
    }

    /// <summary>
    /// Метод попытки установки значения длины пути до этой ячейки из стартовой
    /// </summary>
    /// <param name="currentPathLenght">Новая длина пути, которую необходимо записать</param>
    /// <param name="previousCellCoords">Клетка, из которой пришли в эту клетку</param>
    /// <returns>Успешность попытки записи новой длины пути</returns>
    /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

    public bool TrySetPathLenght(Vector2Int previousCellCoords, int currentPathLenght)
    {
        if (currentPathLenght < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        if (currentPathLenght < pathLength || pathLength == 0)
        {
            this.pathLength = currentPathLenght;
            SetPreviousCell(previousCellCoords);
            return true;
        }
        return false;
    }

    private void SetPreviousCell(Vector2Int pCCoords)
    {
        if (Math.Abs(coordinates.x - pCCoords.x) + Math.Abs(coordinates.y - pCCoords.y) == 1)
        {
            this.previousCellCoords = pCCoords;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// Метод записи эвристического приближения
    /// </summary>
    /// <param name="currentHeuristicApproximation">Рассчитаное значение эвристического приближения, которое необходимо записать</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

    public void SetHeuristicApproximation(int currentHeuristicApproximation)
    {
        if (currentHeuristicApproximation < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        heuristicApproximation = currentHeuristicApproximation;
    }
}
