using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

public class AbstractCellModel
{
    private CellData _cellData;
    private CellState _cellState;

    public CellStateType CellStateType { get; private set; }
    public Vector2Int CellCoordinates { get; private set; }

    public AbstractCellModel(CellData cellData)
    {
        _cellData = cellData;

        CellCoordinates = cellData.CellCoordinates;
    }

    public AbstractCellModel(CellData cellData, Vector2Int cellCoordinates)
    {
        _cellData = cellData;

        CellCoordinates = cellCoordinates;
    }

    public virtual List<CellActionData> GetActionData()
    {
        return _cellState.GetActionData();
    }
}
