using Grid;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbsGridLayer
{
    protected GridPresenter _gridPresenter;

    public AbsGridLayer(GridPresenter presenter)
    {
        _gridPresenter = presenter;
        Refresh();
        Subscribe();
    }

    private void Subscribe()
    {
        _gridPresenter.ReplaceTile += OnReplaceTile;
    }

    protected Dictionary<Vector2Int, T> GetTilesTypeOf<T>() where T : class
    {
        List<AbsTilePresenter> tiles = _gridPresenter.GetAllTiles().Values.ToList();

        Dictionary<Vector2Int, T> specificTiles = new Dictionary<Vector2Int, T>();

        foreach (AbsTilePresenter tile in tiles)
        {
            if (TryGetTileTypeOf(tile, out T specificTile))
            {
                specificTiles.Add(tile.GetTilePosition(), specificTile);
            }
        }

        return specificTiles;
    }

    protected bool TryGetTileTypeOf<T>(AbsTilePresenter tile, out T specificTile) where T : class
    {
        if (tile is T)
        {
            specificTile = tile as T;
            return true;
        }

        specificTile = null;
        return false;
    }

    protected abstract void OnReplaceTile(AbsTilePresenter tile);

    public abstract void Refresh();
}