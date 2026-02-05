using System.Collections.Generic;
using UnityEngine;

namespace Gird
{
    public interface IGrid
    {
        public bool CheckAvaibleCoords(Vector2Int coords);

        public bool TryGetTiles(IReadOnlyList<Vector2Int> tilesPos, out List<AbsTilePresenter> tiles);

        public bool TryReplaceTile(Vector2Int coords, AbsTileConfig tileConfig, out AbsTilePresenter tile);

        public void ChangeVisibility(Vector2Int coords, bool isVisible);

    }
}
