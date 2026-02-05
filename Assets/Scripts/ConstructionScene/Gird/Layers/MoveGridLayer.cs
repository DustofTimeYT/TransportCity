
using Grid;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


/*public class MoveGridLayer : AbsGridLayer, IMoveGrid
{
    private Dictionary<Vector2Int, IMovable> _movableTiles;

    public MoveGridLayer(GridPresenter presenter) : base(presenter)
    {
    }

    protected override void OnReplaceTile(AbsTilePresenter absTile)
    {
        _movableTiles.Remove(absTile.GetTilePosition());

        if (TryGetTileTypeOf<IMovable>(absTile, out IMovable tile))
        {
            _movableTiles.Add(absTile.GetTilePosition(), tile);
        }

        Debug.Log(_movableTiles.Count);
    }

    public override void Refresh()
    {
        _movableTiles = GetTilesTypeOf<IMovable>();
    }

    public bool TryGetTile(Vector2Int tilePos, out IMovable tile)
    {
        return _movableTiles.TryGetValue(tilePos, out tile);
    }
}*/

public class MoveGridLayer : IMoveGrid
{
    private GridLayer<IMovable> _gridLayer;

    public MoveGridLayer(GridPresenter grid)
    {
        _gridLayer = new(grid);
    }

    public bool TryGetTile(Vector2Int tilePos, out IMovable tile)
    {
        return _gridLayer.TryGetTile(tilePos, out tile);
    }
}
