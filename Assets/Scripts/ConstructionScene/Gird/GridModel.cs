using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gird
{
    public class GridModel
    {

        public Dictionary<Vector2Int, AbsTilePresenter> Tiles { get; private set; }

        public GridModel(Dictionary<Vector2Int, AbsTilePresenter> grid)
        {
            Tiles = grid;
        }

        public bool TryReplaceTile(AbsTilePresenter tile)
        {
            var coords = tile.GetTilePosition();
            if (Tiles.ContainsKey(coords))
            {
                Tiles[coords] = tile;
                return true;
            }
            return false;
        }

        /*public bool TryReplaceCell(RoadCellPresenter cell)
        {
            if (!Roads.Contains(cell.GetCellPosition()))
            {
                Roads.Add(cell.GetCellPosition());
                return true;
            }
            return false;
        }*/
    }
}
