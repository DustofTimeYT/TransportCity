using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IMoveable
{
    public void SetCellStateType(CellStateType cellStateType);
    public CellStateType GetCellStateType();
    public void SetMovementDifficulty(int value);
    public int GetMovementDifficulty();
}
