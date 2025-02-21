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
    /// ����� �������� ���� ������
    /// </summary>

    public void CalculateCellWeight()
    {
        cellWeight = pathlength + heuristicApproximation;
    }

    /// <summary>
    /// ����� ������� ��������� �������� ����� ���� �� ���� ������ �� ���������
    /// </summary>
    /// <param name="currentPathLenght">����� ����� ����, ������� ���������� ��������</param>
    /// <param name="previousCell">������, �� ������� ������ � ��� ������</param>
    /// <returns>���������� ������� ������ ����� ����� ����</returns>
    /// <exception cref="ArgumentOutOfRangeException">�������� �� ������ ���� ������ ����</exception>

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
    /// ����� ������ �������������� �����������
    /// </summary>
    /// <param name="currentHeuristicApproximation">����������� �������� �������������� �����������, ������� ���������� ��������</param>
    /// <exception cref="ArgumentOutOfRangeException">�������� �� ������ ���� ������ ����</exception>

    public void SetHeuristicApproximation(int currentHeuristicApproximation)
    {
        if (currentHeuristicApproximation < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        heuristicApproximation = currentHeuristicApproximation;
    }
}
