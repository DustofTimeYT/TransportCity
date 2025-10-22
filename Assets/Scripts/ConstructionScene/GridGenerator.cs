using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator
{
    public static Dictionary<Vector2Int, AbstractCellPresenter> GenerateGridModel(GridConfig gridConfig, CellConfig cellConfig, RoadCellView cellViewPrefab, UIEventBus UIEventBus)
    {
        GameObject GridGO = new GameObject("Grid");
        Dictionary <Vector2Int, AbstractCellPresenter> grid = new();

        for (int x = 0; x < gridConfig.GridSize.x; x++)
        {
            for (int y = 0; y < gridConfig.GridSize.y; y++)
            {
                //RoadCellView cellView = GameObject.Instantiate(cellViewPrefab, GridGO.transform);
                //RoadCellModel cellModel = new RoadCellModel(cellConfig.Data, new Vector2Int(x,y));
                AbstractCellPresenter cellPresenter = new StructureCellPresenter(new Vector2Int(x, y), cellConfig, UIEventBus);

                grid.Add(cellPresenter.GetCellPosition(), cellPresenter);
            }
        }

        return grid;
    }
}
