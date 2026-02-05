
using Grid;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GarageGridLayer : AbsGridLayer
{
    private Dictionary<Vector2Int, IGarage> _tiles;

    public GarageGridLayer(GridPresenter presenter) : base(presenter)
    {
    }

    protected override void OnReplaceTile(AbsTilePresenter absTile)
    {
        _tiles.Remove(absTile.GetTilePosition());

        if (TryGetTileTypeOf<IGarage>(absTile, out IGarage tile))
        {
            _tiles.Add(absTile.GetTilePosition(), tile);
        }

        Debug.Log(_tiles.Count);
    }

    public override void Refresh()
    {
        _tiles = GetTilesTypeOf<IGarage>();
    }

    public bool TryGetTile(Vector2Int tilePos, out IGarage tile)
    {
        return _tiles.TryGetValue(tilePos, out tile);
    }
}
