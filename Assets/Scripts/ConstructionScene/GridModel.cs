using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModel
{
    private GridData _gridData;
    private Vector2Int GridSize;
    private Dictionary<Vector2Int, CellPresenter> _grid;
    private CellPresenter _startCell;
    private CellPresenter _endCell;

    public Dictionary<Vector2Int, CellPresenter> Grid { get => _grid; }
    public CellPresenter StartCell { get => _startCell; }
    public CellPresenter EndCell { get => _endCell; }

    public GridModel( Dictionary<Vector2Int, CellPresenter> grid)
    {
        _grid = grid;
    }

    public void SetStartCell(CellPresenter cell)
    {
        _startCell = cell;
    }

    public void SetEndCell(CellPresenter cell)
    {
        _endCell = cell;
    }

}
