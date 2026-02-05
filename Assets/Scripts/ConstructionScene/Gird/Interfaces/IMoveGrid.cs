using UnityEngine;

public interface IMoveGrid
{
    public bool TryGetTile(Vector2Int tilePos, out IMovable tile);
}
