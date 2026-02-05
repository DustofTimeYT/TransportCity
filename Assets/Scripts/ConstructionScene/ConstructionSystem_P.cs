using Gird;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstructionSystem_P
{
    private readonly IGrid _grid;

    public ConstructionSystem_P(IGrid grid)
    {
        _grid = grid;
    }

    public bool CheckAvaibleCoords(Vector2Int coords)
    {
        return _grid.CheckAvaibleCoords(coords);
    }

    public void ShowTile(Vector3 position)
    {
        ChangeVisibility(position, true);
    }

    public void HideTile(Vector3 position)
    {
        ChangeVisibility(position, false);
    }

    private void ChangeVisibility(Vector3 position3, bool isVisible)
    {
        Vector2Int position2Int = new Vector2Int(Mathf.RoundToInt(position3.x), Mathf.RoundToInt(position3.z));
        if (CheckAvaibleCoords(position2Int))
        {
            _grid.ChangeVisibility(position2Int, isVisible);
        }
    }

    public bool TryPlaceTile(Vector2Int coords, AbsTileConfig tileConfig)
    {
        if (_grid.TryReplaceTile(coords, tileConfig, out AbsTilePresenter tile)) return true;
        return false;
    }
}
