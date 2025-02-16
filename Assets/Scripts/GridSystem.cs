using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public Vector2Int GridSize = Vector2Int.one;

    public GameObject CellViewPrefab;

    public Dictionary<Vector2Int, CellData> GridGenerator()
    {
        var grid = new Dictionary<Vector2Int, CellData>();

        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                CellData cell = new CellData(CellViewPrefab, new Vector2Int(x, y));
                grid.Add(cell.coordinates,cell);
            }
        }

        return grid;
    }
}
