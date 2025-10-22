using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

public class RoadCellModel : AbstractCellModel
{
    private CellData _cellData;
    private CellState _cellState;

    public bool IsInPath { get; private set; }
    public int MovementDifficulty { get; private set; }
    public CellStateType CellStateType { get; private set; }
    public Vector2Int CellCoordinates { get; private set; }

    public RoadCellModel(CellData cellData, Vector2Int coords) : base(cellData, coords)
    {
        _cellData = cellData;

        CellCoordinates = coords;

        MovementDifficulty = _cellData.MovementDifficulty;
        CellStateType = _cellData.CellStateType;
    }

    public List<CellActionData> GetActionData()
    {
        return _cellState.GetActionData();
    }

    public void SetIsInPath(bool value)
    {
         IsInPath = value;
    }

    public void  TrySetMovementDifficulty(int value)
    {
        if (value < 1)
            return;
        MovementDifficulty = value;
    }

    public void SetState(CellStateType cellStateType, RoadCellPresenter cellPresenter)
    {
        CellState newCellState;
        switch(cellStateType)
        {
            case CellStateType.StartCell:
                newCellState = new StartCellState(cellPresenter);
                break;

            case CellStateType.EndCell:
                newCellState = new EndCellState(cellPresenter);
                break;

            case CellStateType.AvailableCell:
                newCellState = new AvailableCellState(cellPresenter);
                break;

            case CellStateType.UnrichmentCell:
                newCellState = new UnrichmentCellState(cellPresenter);
                break;

            default:
                newCellState = null;
                Debug.LogError("No create method for type " + cellStateType);
                break;
        }

        CellStateType = cellStateType;
        _cellState = newCellState;
    }
}
