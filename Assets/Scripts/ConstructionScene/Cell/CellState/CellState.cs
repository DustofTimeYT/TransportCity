using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class CellState
{
    protected CellPresenter _cell;

    public CellState(CellPresenter cell)
    {
        _cell = cell;
    }

    public abstract List<CellActionData> GetActionData();

    public abstract void SetStart();

    public abstract void SetEnd();

    public abstract void SetAvailabel();

    public abstract void SetDisabel();
}
