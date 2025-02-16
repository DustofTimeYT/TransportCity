using System;
using UnityEngine;

public class PathFindingCell
{

    private int _movementDifficulty = 1;

    public PathFindingCell(Vector2Int coordinates, Vector2Int previousCell, int pathLenght, int heuristicApproximation)
    {
        this.coordinates = coordinates;
        SetPathLenght(previousCell, pathLenght);
        SetHeuristicApproximation(heuristicApproximation);
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
        cellWeight = pathlength * _movementDifficulty + heuristicApproximation;
    }

    /// <summary>
    /// Метод попытки установки значения длины пути до этой ячейки из стартовой
    /// </summary>
    /// <param name="currentPathLenght">Новая длина пути, которую необходимо записать</param>
    /// <returns>Успешность попытки записи новой длины пути</returns>
    /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

    public bool TrySetPathLenght(Vector2Int previousCell, int currentPathLenght)
    {
        if (currentPathLenght < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        if (currentPathLenght < pathlength)
        {
            this.pathlength = currentPathLenght;
            this.previousCell = previousCell;
            return true;
        }
        return false;
    }

    private void SetPathLenght(Vector2Int previousCell, int currentPathLenght)
    {
        if (currentPathLenght < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        this.pathlength = currentPathLenght;
        this.previousCell = previousCell;
    }

    /// <summary>
    /// Метод записи эвристического приближения
    /// </summary>
    /// <param name="currentHeuristicApproximation">Рассчитаное значение эвристического приближения, которое необходимо записать</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

    private void SetHeuristicApproximation(int currentHeuristicApproximation)
    {
        if (currentHeuristicApproximation < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        heuristicApproximation = currentHeuristicApproximation;
    }
}
