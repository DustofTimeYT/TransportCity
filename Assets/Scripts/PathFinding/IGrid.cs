using UnityEngine;

public interface IGrid
{
    public bool TryGetCell(Vector2Int cellPos, out IMoveable cell);
}
