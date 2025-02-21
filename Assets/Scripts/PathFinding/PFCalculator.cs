using UnityEngine;

public class PFCalculator
{
    private static int _pathCost = 10;

    /// <summary>
    /// ����� �������� ����� ���� �� ��������� ������
    /// </summary>
    /// <param name="currentCellPathLenght">����� ���� �� �������������� ������</param>
    /// <param name="movementDifficulty">��������� ����������� �� ������� ������</param>
    /// <returns>����� ���� �� ���������� ������</returns>

    public static int CalculatePathLenght(int currentCellPathLenght, int movementDifficulty)
    {
        return currentCellPathLenght + _pathCost * movementDifficulty;
    }

    /// <summary>
    /// ����� �������� �������������� ����������� � ������� �������������� ���������� 
    /// </summary>
    /// <param name="currentCell">���������� (x, y) ������ ��� ������� �������������� �����������</param>
    /// <param name="endCell">���������� (x, y) ������� ������</param>
    /// <returns>������������� ����������� ������� ������</returns>

    public static int CalculateHeuristicApproximation(Vector2Int currentCell, Vector2Int endCell)
    {
        return (Mathf.Abs(currentCell.x - endCell.x) + Mathf.Abs(currentCell.y - endCell.y)) * _pathCost;
    }
}
