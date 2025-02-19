using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class StartCellState : CellState
{
    public StartCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(CellActionType.End, SetEnd),
            new CellActionData(CellActionType.Availabel, SetAvailabel),
            new CellActionData(CellActionType.Disable, SetDisabel)
        };
    }

    public override void SetStart()
    {
        // не возможно сделать ее еще раз стартовой клеткой
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
