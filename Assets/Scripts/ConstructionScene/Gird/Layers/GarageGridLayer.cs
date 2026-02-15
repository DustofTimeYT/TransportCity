using Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GarageGridLayer : IGarageGrid
{
    private GridLayer<IGarage> _gridLayer;

    public GarageGridLayer(GridPresenter grid)
    {
        _gridLayer = new(grid);

        Subscribe();
        Debug.Log($"{this} was created");
    }

    private void Subscribe()
    {
        _gridLayer.UpdateLayer += OnUpdateLayer;
    }

    private void OnUpdateLayer()
    {
        UpdateLayer?.Invoke();
    }

    public Dictionary<Vector2Int, IGarage> GetGarages()
    {
        return _gridLayer.GetTiles();
    }

    public bool TryGetTile(Vector2Int tilePos, out IGarage tile)
    {
        return _gridLayer.TryGetTile(tilePos, out tile);
    }
    
    public event Action UpdateLayer;
}
