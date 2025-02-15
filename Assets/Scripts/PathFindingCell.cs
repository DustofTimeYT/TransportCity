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
