using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class UnrichmentCellState : CellState
{
    public UnrichmentCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(CellActionType.Availabel, SetAvailabel)
        };
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
