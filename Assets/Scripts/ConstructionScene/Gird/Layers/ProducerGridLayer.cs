
using Grid;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ProducerGridLayer : AbsGridLayer
{
    private Dictionary<Vector2Int, IProducer> _tiles;

    public ProducerGridLayer(GridPresenter presenter) : base(presenter)
    {
    }

    protected override void OnReplaceTile(AbsTilePresenter absTile)
    {
        _tiles.Remove(absTile.GetTilePosition());

        if (TryGetTileTypeOf<IProducer>(absTile, out IProducer tile))
        {
            _tiles.Add(absTile.GetTilePosition(), tile);
        }

        Debug.Log(_tiles.Count);
    }

    public override void Refresh()
    {
        _tiles = GetTilesTypeOf<IProducer>();
    }

    public bool TryGetTile(Vector2Int tilePos, out IProducer tile)
    {
        return _tiles.TryGetValue(tilePos, out tile);
    }
}
