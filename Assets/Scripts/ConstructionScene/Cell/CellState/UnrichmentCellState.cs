using System.Collections.Generic;

/// <summary>
/// ������������� �����-��������� �� ��������� ��� ����������� ������
/// </summary>

sealed class UnrichmentCellState : CellState
{
    public UnrichmentCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(UITypeAction.Button, CellActionType.Availabel, SetAvailabel)
        };
    }

    public override void SetMovementDifficulty(string value)
    {
        // �� �������� ���������� ��������� ����������� ��� ���� ������
    }

    public override void SetStart()
    {
        // �� �������� ������� ��������� ������� ���� ��� �� ��������
    }

    public override void SetEnd()
    {
        // �� �������� ������� ������� ������� ���� ��� �� ��������
    }

    public override void SetAvailabel()
    {
        _cell.SetCellStateType(CellStateType.AvailableCell);
    }

    public override void SetDisabel()
    {
        // �� �������� ������� �� ��� ��� �� ��������� ��� �����������
    }
}
