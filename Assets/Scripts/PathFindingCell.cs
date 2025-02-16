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
    /// ����� �������� ���� ������
    /// </summary>

    public void CalculateCellWeight()
    {
        cellWeight = pathlength * _movementDifficulty + heuristicApproximation;
    }

    /// <summary>
    /// ����� ������� ��������� �������� ����� ���� �� ���� ������ �� ���������
    /// </summary>
    /// <param name="currentPathLenght">����� ����� ����, ������� ���������� ��������</param>
    /// <returns>���������� ������� ������ ����� ����� ����</returns>
    /// <exception cref="ArgumentOutOfRangeException">�������� �� ������ ���� ������ ����</exception>

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
    /// ����� ������ �������������� �����������
    /// </summary>
    /// <param name="currentHeuristicApproximation">����������� �������� �������������� �����������, ������� ���������� ��������</param>
    /// <exception cref="ArgumentOutOfRangeException">�������� �� ������ ���� ������ ����</exception>

    private void SetHeuristicApproximation(int currentHeuristicApproximation)
    {
        if (currentHeuristicApproximation < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        heuristicApproximation = currentHeuristicApproximation;
    }
}
