using System.Collections.Generic;
using UnityEngine;

namespace Gird
{
    public interface IGrid
    {
        public bool CheckAvaibleCoords(Vector2Int coords);

        public bool TryGetTiles(IReadOnlyList<Vector2Int> tilesPos, out List<ITilePresenter> tiles);

        public bool TryReplaceTile(Vector2Int coords, int rotationAngle, AbsTileConfig tileConfig, out ITilePresenter tile);

        public void ChangeVisibility(Vector2Int coords, bool isVisible);

    }
}
