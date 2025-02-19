using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator
{
    public static Dictionary<Vector2Int, CellPresenter> GenerateGridModel(GridConfig gridConfig, CellConfig cellConfig, CellView cellViewPrefab, UIEventBus UIEventBus)
    {
        GameObject GridGO = new GameObject("Grid");
        Dictionary <Vector2Int, CellPresenter> grid = new();

        for (int x = 0; x < gridConfig.GridSize.x; x++)
        {
            for (int y = 0; y < gridConfig.GridSize.y; y++)
            {
                CellView cellView = GameObject.Instantiate(cellViewPrefab, GridGO.transform);
                CellModel cellModel = new CellModel(cellConfig.Data, cellView, new Vector2Int(x,y));
                CellPresenter cellPresenter = new CellPresenter(cellModel, UIEventBus);

                cellView.name = $"{cellView.name} {cellModel.CellCoordinates}";
                cellView.Init(cellPresenter, cellModel.CellCoordinates);

                grid.Add(cellModel.CellCoordinates, cellPresenter);
            }
        }

        return grid;
    }
}
