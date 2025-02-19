using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class EndCellState : CellState
{
    public EndCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(CellActionType.Start, SetStart),
            new CellActionData(CellActionType.Availabel, SetAvailabel),
            new CellActionData(CellActionType.Disable, SetDisabel)
        };
    }

    public override void SetStart()
    {
        _cell.SetCellStateType(CellStateType.StartCell);
    }

    public override void SetEnd()
    {
        // не возможно сделать ее еще раз целевой клеткой
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
