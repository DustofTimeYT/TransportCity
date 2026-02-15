
using Grid;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class MoveGridLayer : IMoveGrid
{
    private GridLayer<IMovable> _gridLayer;

    public MoveGridLayer(GridPresenter grid)
    {
        _gridLayer = new(grid);
        Debug.Log($"{this} was created");
    }

    public bool TryGetTile(Vector2Int tilePos, out IMovable tile)
    {
        return _gridLayer.TryGetTile(tilePos, out tile);
    }
}
