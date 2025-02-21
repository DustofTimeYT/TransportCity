using System.Collections.Generic;

/// <summary>
/// ������������� �����-��������� ��������� ������, � ���� ������ ���������� ����� ����
/// </summary>

sealed class StartCellState : CellState
{
    public StartCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(UITypeAction.Button, CellActionType.End, SetEnd),
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
        // �� �������� ������� �� ��� ��� ��������� �������
    }

    public override void SetEnd()
    {
        _cell.SetCellStateType(CellStateType.EndCell);
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
