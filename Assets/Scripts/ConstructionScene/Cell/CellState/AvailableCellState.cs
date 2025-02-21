using System.Collections.Generic;

/// <summary>
/// Ненаследуемый класс-состояние клетки, которые может использовать алгоритм для построения маршрута
/// </summary>

sealed class AvailableCellState : CellState
{
    public AvailableCellState(CellPresenter cell) : base(cell)
    {
    }

    public override List<CellActionData> GetActionData()
    {
        return new List<CellActionData>()
        {
            new CellActionData(UITypeAction.InputField, CellActionType.MovementDifficulty, SetMovementDifficulty),
            new CellActionData(UITypeAction.Button, CellActionType.Start, SetStart),
            new CellActionData(UITypeAction.Button, CellActionType.End, SetEnd),
            new CellActionData(UITypeAction.Button, CellActionType.Disable, SetDisabel)
        };
    }

    public override void SetMovementDifficulty(string value)
    {
        if (int.TryParse(value, out int i))
        {
            _cell.SetMovementDifficulty(i);
        }
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
