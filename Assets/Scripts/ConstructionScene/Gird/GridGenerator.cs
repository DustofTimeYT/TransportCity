using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator
{
    private GridConfig _gridConfig;
    private TilesConfig _tilesConfig;
    public GridGenerator(GridConfig gridConfig, TilesConfig tilesConfig)
    {
        _gridConfig = gridConfig;
        _tilesConfig = tilesConfig;
    }

    public Dictionary<Vector2Int, AbsTilePresenter> GenerateGridModel(Transform GridTransform)
    {
        Dictionary <Vector2Int, AbsTilePresenter> grid = new();

        for (int x = 0; x < _gridConfig.GridSize.x; x++)
        {
            for (int y = 0; y < _gridConfig.GridSize.y; y++)
            {
                AbsTilePresenter cellPresenter = new StructureTilePresenter(new Vector2Int(x, y), GetDefaultTile(), GridTransform);

                grid.Add(cellPresenter.GetTilePosition(), cellPresenter);
            }
        }

        return grid;
    }

    private AbsTileConfig GetDefaultTile()
    {
        var defTiles = _tilesConfig.GetDefaultTiles();

        return defTiles[Random.Range(0, defTiles.Count-1)];
    }
}
