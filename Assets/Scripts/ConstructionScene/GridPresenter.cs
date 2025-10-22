using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridPresenter: IGrid
{
    private GridModel _gridModel;

    private UIEventBus _UIEventBus;

    public GridPresenter(UIEventBus UIEventBus, GridModel gridModel)
    {
        _gridModel = gridModel;
        _UIEventBus = UIEventBus;
        Subscribe();
    }

    public void SetStartCell(RoadCellPresenter cell)
    {
        if (_gridModel.StartCell != null)
        {
            _gridModel.StartCell.SetCellStateType(CellStateType.AvailableCell);
        }

        _gridModel.SetStartCell(cell);
    }

    public void SetEndCell(RoadCellPresenter cell)
    {
        if (_gridModel.EndCell != null)
        {
            _gridModel.EndCell.SetCellStateType(CellStateType.AvailableCell);
        }

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

    private void OnSetStartCell(RoadCellPresenter startCell)
    {
        SetStartCell(startCell);
    }

    private void OnSetEndCell(RoadCellPresenter endCell)
    {
        SetEndCell(endCell);
    }
    
    public IEnumerator DisplayPath(IReadOnlyList<Vector2Int> path)
    {
        if (path == null) { yield break; }
        /*
        if (TryGetCells(path, out List<RoadCellPresenter> cells))
        {
            foreach (var cell in cells)
            {
                cell.HighlightCell();
                yield return new WaitForSeconds(0.1f);
            }
        }
        */
        Debug.LogError("Dont work now");
        yield return new WaitForSeconds(1f);

        ResetGrid(path);
        yield break;
    }
    
    public bool TryReplaceCell(Vector2Int coords, CellConfig cellConfig, out AbstractCellPresenter cell)
    {
        if (!TryGetCell(coords, out cell))
        {
            Debug.LogError("Вы находитесь за пределами поля");
            return false;
        }

        if (cell != null)
        {
            switch (cellConfig.CellType) 
            {
                case CellType.Road:
                    _gridModel.TryReplaceCell(new RoadCellPresenter(cell.GetCellPosition(), cellConfig, _UIEventBus));
                    cell.Delete();
                    break;

                case CellType.Structure:
                    _gridModel.TryReplaceCell(new StructureCellPresenter(cell.GetCellPosition(), cellConfig, _UIEventBus));
                    cell.Delete();
                    break;
            }
        }

        return true;
    }

    public bool CheckAvaibleCoords(Vector2Int coords)
    {
        return TryGetCell(coords, out AbstractCellPresenter cell);
    }

    public void ResetGrid(IReadOnlyList<Vector2Int> path)
    {
        if (path == null) { return; }

        if (TryGetCells(path, out List<AbstractCellPresenter> cells))
        {
            foreach (var cell in cells)
            {
                //cell.ResetHighlightCell();
            }
            _gridModel.SetStartCell(null);
            _gridModel.SetEndCell(null);
        }
    }

    private bool TryGetCells(IReadOnlyList<Vector2Int> cellsPos, out List<AbstractCellPresenter> cells)
    {
        cells = new();

        foreach (Vector2Int cellPos in cellsPos)
        {
            if (!TryGetCell(cellPos, out AbstractCellPresenter cell)) return false;
            cells.Add(cell);
        }

        return true;
    }

    private bool TryGetCells(IReadOnlyList<Vector2Int> cellsPos, out List<IMoveable> cells)
    {
        cells = new();

        foreach (Vector2Int cellPos in cellsPos)
        {
            if (!TryGetCell(cellPos, out IMoveable cell)) return false;
            cells.Add(cell);
        }

        return true;
    }

    private bool TryGetCell(Vector2Int cellPos, out AbstractCellPresenter cell)
    {
        cell = null;
        if (_gridModel.Structures.TryGetValue(cellPos, out var _structure))
        {
            cell = _structure;
            return true;
        }
        if (_gridModel.Roads.TryGetValue(cellPos, out var _road))
        {
            cell = _road;
            return true;
        }
        if (_gridModel.Producers.TryGetValue(cellPos, out var _producer))
        {
            //cell = _producer;
            Debug.LogError("Производитель не доделан");
            return true;
        }
        if (_gridModel.Consumers.TryGetValue(cellPos, out var _consumer))
        {
            //cell = _consumer;
            Debug.LogError("Потребитель не доделан");
            return true;
        }
        return false;
    }

    public bool TryGetCell(Vector2Int cellPos, out IMoveable cell)
    {
        if (_gridModel.Roads.TryGetValue(cellPos, out var RCPcell))
        {
            cell = RCPcell;
            return true;
        }

        cell = null;
        return false;
    }

}
