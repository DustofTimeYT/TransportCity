using System.Collections.Generic;

/// <summary>
/// Ненаследуемый класс-состояние целевой клетки, по достижении этой клетки поиск пути заканчивается
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
        // не возможно установить сложность перемещения для этой клетки
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
