using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

public class RoadCellPresenter : AbstractCellPresenter, IMoveable
{
    readonly private RoadCellModel _cellModel;

    public RoadCellPresenter(RoadCellModel cellModel, UIEventBus UIEventBus)
    {
        _cellModel = cellModel;
        _cellModel.SetState(_cellModel.CellStateType, this);
        _UIEventBus = UIEventBus;
    }

    public RoadCellPresenter(Vector2Int coords, CellConfig cellConfig, UIEventBus UIEventBus)
    {
        if (cellConfig.CellType != CellType.Road) return;
        _cellModel = new RoadCellModel(cellConfig.Data, coords);
        InstantiateView(cellConfig.CellPref);
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

        UpdateView?.Invoke(_cellModel);
    }
    ///удалить..
    public void HighlightCell()
    {
        _cellModel.SetIsInPath(true);
        UpdateView?.Invoke(_cellModel);
    }

    public void ResetHighlightCell()
    {
        _cellModel.SetIsInPath(false);
        SetCellStateType(CellStateType.AvailableCell);
    }
    ///..
    public int GetMovementDifficulty()
    {
        return _cellModel.MovementDifficulty;
    }

    public void SetMovementDifficulty( int value)
    {
        _cellModel.TrySetMovementDifficulty(value);
        UpdateView?.Invoke(_cellModel);
    }

    public override Vector2Int GetCellPosition()
    {
        return _cellModel.CellCoordinates;
    }

    public override List<CellActionData> GetActionData()
    {
        return _cellModel.GetActionData();
    }

    public override event Action<AbstractCellModel> UpdateView;
}
