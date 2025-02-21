using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

public class CellPresenter
{
    private CellModel _cellModel;
    private UIEventBus _UIEventBus;

    public CellPresenter(CellModel cellModel, UIEventBus UIEventBus)
    {
        _cellModel = cellModel;
        _cellModel.SetState(_cellModel.CellStateType, this);
        _UIEventBus = UIEventBus;
    }

    public CellStateType GetCellStateType()
    {
        return _cellModel.CellStateType;
    }

    public void SetCellStateType(CellStateType cellStateType)
    {
        _cellModel.SetState(cellStateType, this);

        if (cellStateType == CellStateType.StartCell)
        {
            _UIEventBus.TriggerSetStartCell(this);
        }
        if (cellStateType == CellStateType.EndCell)
        {
            _UIEventBus.TriggerSetEndCell(this);
        }

        _cellModel.UpdateView();
    }

    public void HighlightCell()
    {
        _cellModel.SetIsInPath(true);
    }

    public void ResetHighlightCell()
    {
        _cellModel.SetIsInPath(false);
        SetCellStateType(CellStateType.AvailableCell);
    }

    public Vector2Int GetCellPosition()
    {
        return _cellModel.CellCoordinates;
    }

    public List<CellActionData> GetActionData()
    {
        return _cellModel.GetActionData();
    }

    public int GetMovementDifficulty()
    {
        return _cellModel.MovementDifficulty;
    }

    public void SetMovementDifficulty( int value)
    {
        _cellModel.SetMovementDifficulty(value);
    }

    public void UpdateView()
    {
        _cellModel.UpdateView();
    }
}
