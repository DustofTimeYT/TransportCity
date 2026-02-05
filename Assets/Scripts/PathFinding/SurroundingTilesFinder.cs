
using System.Collections.Generic;
using UnityEngine;

namespace PathFindAlgo
{
    public class SurroundingTilesFinder : ISurroundingTilesFinder
    {
        /// <summary>
        /// ћетод поиска €чеек вокруг текущей €чейки
        /// </summary>
        /// <param name="currentTileCoordinates"> оординаты (x, y) текущей €чейки</param>
        /// <param name="grid">—писок €чеек, отображаемых пользователю</param>
        /// <param name="openedList">—писок €чеек, которые были обнаружены на прошлом этапе исследовани€<param>
        /// <returns>—писок клеток наход€щихс€ вокруг переданной клетки в форме +</returns>

        public IReadOnlyList<PathFindingTile> FindSurroundingTiles(Vector2Int currentTileCoordinates, IMoveGrid grid, Dictionary<Vector2Int, PathFindingTile> openedList)
        {
            List<PathFindingTile> surroundingTiles = new List<PathFindingTile>();
            Vector2Int tile;
            for (int i = -1; i <= 1; i += 2)
            {
                tile = new(currentTileCoordinates.x + i, currentTileCoordinates.y);
                CheckTile(tile, surroundingTiles, grid, openedList);

                tile = new(currentTileCoordinates.x, currentTileCoordinates.y + i);
                CheckTile(tile, surroundingTiles, grid, openedList);
            }
            return surroundingTiles;
        }

        private void CheckTile(Vector2Int tile, List<PathFindingTile> surroundingTiles, IMoveGrid grid, Dictionary<Vector2Int, PathFindingTile> openedList)
        {
            if (openedList.TryGetValue(new Vector2Int(tile.x, tile.y), out PathFindingTile PFTile))
            {
                surroundingTiles.Add(PFTile);
            }

            if (grid.TryGetTile(new Vector2Int(tile.x, tile.y), out IMovable tileData))
            {
                surroundingTiles.Add(new PathFindingTile(tile, tileData.GetMovementDifficulty()));
            }
        }
    }
}
