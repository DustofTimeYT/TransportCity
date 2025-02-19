using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridPresenter
{
    private GridModel _gridModel;

    private IReadOnlyUIEventBus _UIEventBus;

    public GridPresenter(IReadOnlyUIEventBus UIEventBus, GridModel gridModel)
    {
        _gridModel = gridModel;
        _UIEventBus = UIEventBus;
        Subscribe();
    }

    public void SetStartCell(CellPresenter cell)
    {
        if (_gridModel.StartCell != null)
        {
            _gridModel.StartCell.SetCellStateType(CellStateType.AvailableCell);
        }

        cell.SetCellStateType(CellStateType.StartCell);
        _gridModel.SetStartCell(cell);
    }

    public void SetEndCell(CellPresenter cell)
    {
        if (_gridModel.EndCell != null)
        {
            _gridModel.EndCell.SetCellStateType(CellStateType.AvailableCell);
        }

        cell.SetCellStateType(CellStateType.EndCell);
        _gridModel.SetEndCell(cell);
    }

    private void Subscribe()
    {
        _UIEventBus.SetStartCell += OnSetStartCell;
        _UIEventBus.SetEndCell += OnSetEndCell;
    }

    private void Unsubscribe()
    {
        _UIEventBus.SetStartCell -= OnSetStartCell;
        _UIEventBus.SetEndCell -= OnSetEndCell;
    }

    private void OnSetStartCell(CellPresenter startCell)
    {
        _gridModel.SetStartCell(startCell);
    }

    private void OnSetEndCell(CellPresenter endCell)
    {
        _gridModel.SetEndCell(endCell);
    }
    
    public IEnumerator DisplayPath(IReadOnlyList<Vector2Int> path)
    {
        if (path == null) { yield break; }

        if (TryGetCells(path, out List<CellPresenter> cells))
        {
            foreach (var cell in cells)
            {
                cell.HighlightCell();
                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(1f);

        ResetGrid(path);
        yield break;
    }
    
    public void ResetGrid(IReadOnlyList<Vector2Int> path)
    {
        if (path == null) { return; }

        if (TryGetCells(path, out List<CellPresenter> cells))
        {
            foreach (var cell in cells)
            {
                cell.ResetHighlightCell();
            }
            _gridModel.SetStartCell(null);
            _gridModel.SetEndCell(null);
        }
    }

    private bool TryGetCells(IReadOnlyList<Vector2Int> cellsPos, out List<CellPresenter> cells)
    {
        var grid = _gridModel.Grid;
        cells = new List<CellPresenter>();

        foreach (Vector2Int cellPos in cellsPos)
        {
            if (!grid.TryGetValue(cellPos, out CellPresenter cell)) return false;
            cells.Add(cell);
        }

        return true;
    }
    
}
