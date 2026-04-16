using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gird
{
    public class GridModel
    {

        public Dictionary<Vector2Int, ITilePresenter> Tiles { get; private set; }

        public GridModel(Dictionary<Vector2Int, ITilePresenter> grid)
        {
            Tiles = grid;
        }

        public bool TryReplaceTile(ITilePresenter tile)
        {
            var coords = tile.GetTilePosition();
            if (Tiles.ContainsKey(coords))
            {
                Tiles[coords] = tile;
                return true;
            }
            return false;
        }
    }
}
