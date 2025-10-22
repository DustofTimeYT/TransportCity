using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConstructionSystem_P
{
    private readonly ConstructionSystem_V _cS_V;
    private readonly GridPresenter _grid;

    public ConstructionSystem_P(GridPresenter grid)
    {
        _grid = grid;
    }

    public bool CheckAvaibleCoords(Vector2Int coords)
    {
        return _grid.CheckAvaibleCoords(coords);
    }

    public bool TryPlaceBuilding(Vector2Int coords, CellConfig cellConfig, out AbstractCellPresenter cell)
    {
        /*if (!CheckAvaibleCoords(coords))
        {
            cell = null;
            return false;
        }*/

        if (_grid.TryReplaceCell(coords, cellConfig, out cell)) return true;
        return false;
    }
}
