using Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridLayer<T> where T : class
{
    private GridPresenter _grid;
    private Dictionary<Vector2Int, T> _tiles;

    public GridLayer(GridPresenter presenter)
    {
        _grid = presenter;
        _tiles = new Dictionary<Vector2Int, T>();
        Refresh();
        Subscribe();
    }

    private void Subscribe()
    {
        _grid.ReplaceTile += OnReplaceTile;
    }

    private void Refresh()
    {
        List<AbsTilePresenter> tiles = _grid.GetAllTiles().Values.ToList();

        foreach (AbsTilePresenter tile in tiles)
        {
            if (TryGetTile(tile, out T specificTile))
            {
                _tiles.Add(tile.GetTilePosition(), specificTile);
            }
        }
    }

    private bool TryGetTile(AbsTilePresenter tile, out T specificTile)
    {
        if (tile is T)
        {
            specificTile = tile as T;
            return true;
        }

        specificTile = null;
        return false;
    }

    private void OnReplaceTile(AbsTilePresenter absTile)
    {
        bool IsUpdate = false;

        if (_tiles.Remove(absTile.GetTilePosition()))
        {
            IsUpdate = true;
        }

        if (TryGetTile(absTile, out T tile))
        {
            _tiles.Add(absTile.GetTilePosition(), tile);
            IsUpdate = true;
        }

        IsChangedLayer(IsUpdate);
    }

    private void IsChangedLayer(bool IsUpdate)
    {
        if (IsUpdate)
        {
            UpdateLayer?.Invoke();
        }
    }

    public bool TryGetTile(Vector2Int tilePos, out T tile)
    {
        return _tiles.TryGetValue(tilePos, out tile);
    }

    public Dictionary<Vector2Int, T> GetTiles()
    {
        return _tiles;
    }

    public event Action UpdateLayer;
}
