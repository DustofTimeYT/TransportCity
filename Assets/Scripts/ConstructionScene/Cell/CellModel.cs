using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

public class CellModel
{
    private CellData _cellData;
    private CellView _cellView;
    private Vector2Int _size;
    private CellState _cellState;

    public bool IsInPath { get; private set; }
    public int MovementDifficulty { get; private set; }
    public CellStateType CellStateType { get; private set; }
    public Vector2Int CellCoordinates { get; private set; }

    public CellModel(CellData cellData, CellView cellView)
    {
        _cellData = cellData;
        _cellView = cellView;

        CellCoordinates = cellData.CellCoordinates;

        _size = _cellData.Size;
        MovementDifficulty = _cellData.MovementDifficulty;
        CellStateType = _cellData.CellStateType;
    }

    public CellModel(CellData cellData, CellView cellView, Vector2Int cellCoordinates)
    {
        _cellData = cellData;
        _cellView = cellView;

        CellCoordinates = cellCoordinates;

        _size = _cellData.Size;
        MovementDifficulty = _cellData.MovementDifficulty;
        CellStateType = _cellData.CellStateType;
    }

    public List<CellActionData> GetActionData()
    {
        return _cellState.GetActionData();
    }

    public void UpdateView()
    {
        _cellView.UpdateView(this);
    }

    public void SetIsInPath(bool value)
    {
         IsInPath = value;
        UpdateView();
    }

    public void SetMovementDifficulty(int value)
    {
        if (value < 1)
            return;
        MovementDifficulty = value;
        UpdateView();
    }

    public void SetState(CellStateType cellStateType, CellPresenter cellPresenter)
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
