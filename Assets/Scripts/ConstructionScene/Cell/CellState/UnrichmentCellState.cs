using System.Collections.Generic;

/// <summary>
/// Ненаследуемый класс-состояние не доступной для перемещения клетки
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
        // не возможно установить сложность перемещения для этой клетки
    }

    public override void SetStart()
    {
        // не возможно сделать стартовой клеткой пока она не доступна
    }

    public override void SetEnd()
    {
        // не возможно сделать целевой клеткой пока она не доступна
    }

    public override void SetAvailabel()
    {
        _cell.SetCellStateType(CellStateType.AvailableCell);
    }

    public override void SetDisabel()
    {
        // не возможно сделать ее еще раз не доступной для перемещения
    }
}
