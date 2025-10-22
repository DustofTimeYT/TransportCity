using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StructureCellPresenter : AbstractCellPresenter
{
    readonly private StructureCellModel _cellModel;

    public StructureCellPresenter(Vector2Int coords, CellConfig cellConfig, UIEventBus UIEventBus)
    {
        if (cellConfig.CellType != CellType.Structure) return;
        _cellModel = new StructureCellModel(cellConfig.Data, coords);
        InstantiateView(cellConfig.CellPref);
    }

    public override event Action<AbstractCellModel> UpdateView;

    public override List<CellActionData> GetActionData()
    {
        throw new NotImplementedException();
    }

    public override Vector2Int GetCellPosition()
    {
        return _cellModel.CellCoordinates;
    }
}
