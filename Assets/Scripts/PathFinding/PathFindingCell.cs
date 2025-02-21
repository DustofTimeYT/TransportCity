using System;
using UnityEngine;

public class PathFindingCell
{

    public int movementDifficulty {  get; private set; }

    public PathFindingCell(Vector2Int coordinates, int movementDifficulty)
    {
        this.coordinates = coordinates;
        this.movementDifficulty = movementDifficulty;
        pathlength = 0;
    }

    public Vector2Int coordinates {  get; private set; }

    public Vector2Int previousCell { get; private set; }
    public int cellWeight { get; private set; }
    public int pathlength { get; private set; }
    public int heuristicApproximation { get; private set; }

    /// <summary>
    /// Метод рассчета веса ячейки
    /// </summary>

    public void CalculateCellWeight()
    {
        cellWeight = pathlength + heuristicApproximation;
    }

    /// <summary>
    /// Метод попытки установки значения длины пути до этой ячейки из стартовой
    /// </summary>
    /// <param name="currentPathLenght">Новая длина пути, которую необходимо записать</param>
    /// <param name="previousCell">Клетка, из которой пришли в эту клетку</param>
    /// <returns>Успешность попытки записи новой длины пути</returns>
    /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

    public bool TrySetPathLenght(Vector2Int previousCell, int currentPathLenght)
    {
        if (currentPathLenght < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        if (currentPathLenght < pathlength || pathlength == 0)
        {
            this.pathlength = currentPathLenght;
            this.previousCell = previousCell;
            return true;
        }
        return false;
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
