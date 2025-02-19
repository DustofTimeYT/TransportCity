using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class AvailableCellState : CellState
{
    public AvailableCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(CellActionType.Start, SetStart),
            new CellActionData(CellActionType.End, SetEnd),
            new CellActionData(CellActionType.Disable, SetDisabel)
        };
    }

    public override void SetStart()
    {
        _cell.SetCellStateType(CellStateType.StartCell);
    }

    public override void SetEnd()
    {
        _cell.SetCellStateType(CellStateType.EndCell);
    }

    public override void SetAvailabel()
    {
        // не возможно сделать ее еще раз доступной для перемещения
    }

    public override void SetDisabel()
    {
        _cell.SetCellStateType(CellStateType.UnrichmentCell);
    }
}
