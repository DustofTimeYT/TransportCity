using System;
using UnityEngine;

public class PathFindingCell
{

    private int _movementDifficulty = 1;

    public PathFindingCell(Vector2Int coordinates, int heuristicApproximation)
    {
        this.coordinates = coordinates;
        SetHeuristicApproximation(heuristicApproximation);
    }

    public Vector2Int coordinates {  get; private set; }
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

    public bool TrySetPathLenght(int currentPathLenght)
    {
        if (currentPathLenght < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        if (currentPathLenght < pathlength)
        {
            pathlength = currentPathLenght;
            return true;
        }
        return false;
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
