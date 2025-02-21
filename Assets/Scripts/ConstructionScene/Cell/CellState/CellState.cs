using System.Collections.Generic;

/// <summary>
/// Абстрактное представление класса-состояния
/// </summary>

public abstract class CellState
{
    protected CellPresenter _cell;

    public CellState(CellPresenter cell)
    {
        _cell = cell;
    }

    public abstract List<CellActionData> GetActionData();

    public abstract void SetMovementDifficulty(string value);

    public abstract void SetStart();

    public abstract void SetEnd();

    public abstract void SetAvailabel();

    public abstract void SetDisabel();
}
