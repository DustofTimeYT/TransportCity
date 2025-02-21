using System.Collections.Generic;

/// <summary>
/// ������������� �����-��������� ������� ������, �� ���������� ���� ������ ����� ���� �������������
/// </summary>

sealed class EndCellState : CellState
{
    public EndCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(UITypeAction.Button, CellActionType.Start, SetStart),
            new CellActionData(UITypeAction.Button, CellActionType.Availabel, SetAvailabel),
            new CellActionData(UITypeAction.Button, CellActionType.Disable, SetDisabel)
        };
    }

    public override void SetMovementDifficulty(string value)
    {
        // �� �������� ���������� ��������� ����������� ��� ���� ������
    }

    public override void SetStart()
    {
        _cell.SetCellStateType(CellStateType.StartCell);
    }

    public override void SetEnd()
    {
        // �� �������� ������� �� ��� ��� ������� �������
    }

    public override void SetAvailabel()
    {
        _cell.SetCellStateType(CellStateType.AvailableCell);
    }

    public override void SetDisabel()
    {
        _cell.SetCellStateType(CellStateType.UnrichmentCell);
    }
}
