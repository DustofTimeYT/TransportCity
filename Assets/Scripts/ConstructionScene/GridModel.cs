using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModel
{
    private RoadCellPresenter _startCell;
    private RoadCellPresenter _endCell;

    public Dictionary<Vector2Int, AbstractCellPresenter> Structures { get; private set; }

    public Dictionary<Vector2Int, RoadCellPresenter> Roads { get; private set; }

    public Dictionary<Vector2Int, IConsumer> Consumers { get; private set; }

    public Dictionary<Vector2Int, IProducer> Producers { get; private set; }
    public RoadCellPresenter StartCell { get => _startCell; }
    public RoadCellPresenter EndCell { get => _endCell; }

    public GridModel( Dictionary<Vector2Int, AbstractCellPresenter> grid)
    {
        Structures = grid;
        Roads = new();
        Consumers = new();
        Producers = new();
    }

    public void SetStartCell(RoadCellPresenter cell)
    {
        _startCell = cell;
    }

    public void SetEndCell(RoadCellPresenter cell)
    {
        _endCell = cell;
    }

    public bool TryReplaceCell(RoadCellPresenter cell)
    {
        if (TryDeleteCell(cell.GetCellPosition()))
        {
            return Roads.TryAdd(cell.GetCellPosition(), cell);
        }
        return false;
    }

    public bool TryReplaceCell(StructureCellPresenter cell)
    {
        if (TryDeleteCell(cell.GetCellPosition()))
        {
            return Structures.TryAdd(cell.GetCellPosition(), cell);
        }
        return false;
    }


    private bool TryDeleteCell(Vector2Int coords)
    {
        if (Structures.Remove(coords)) return true;

        if (Roads.Remove(coords)) return true;

        if (Consumers.Remove(coords)) return true;

        if (Producers.Remove(coords)) return true;

        return false;
    }
}
