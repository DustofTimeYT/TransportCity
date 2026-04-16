
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace PathFindAlgo
{
    public class SurroundingTilesFinder : ISurroundingTilesFinder
    {
        private IMoveGrid _grid;
        public void SetGrid(IMoveGrid grid)
        {
            _grid = grid;
        }

        public PathFindingTile FindCurrentTile(Vector2Int currentTileCoordinates)
        {
            if (_grid.TryGetTile(new Vector2Int(currentTileCoordinates.x, currentTileCoordinates.y), out IMovable tileData))
            {
                return new PathFindingTile(currentTileCoordinates, tileData.GetMovementDifficulty(), tileData.GetAvaibleDirections());
            }

            return null;
        }

        /// <summary>
        /// ћетод поиска €чеек вокруг текущей €чейки
        /// </summary>
        /// <param name="currentTileCoordinates"> оординаты (x, y) текущей €чейки</param>
        /// <param name="grid">—писок €чеек, отображаемых пользователю</param>
        /// <param name="openedList">—писок €чеек, которые были обнаружены на прошлом этапе исследовани€<param>
        /// <returns>—писок клеток наход€щихс€ вокруг переданной клетки в форме +</returns>

        public IReadOnlyList<PathFindingTile> FindSurroundingTiles(Vector2Int currentTileCoordinates, Dictionary<Vector2Int, PathFindingTile> openedList)
        {
            List<PathFindingTile> surroundingTiles = new List<PathFindingTile>();
            Vector2Int tileCoords;
            openedList.TryGetValue(currentTileCoordinates, out PathFindingTile currentPFTile);
            for (int Xi = -1; Xi <= 1; Xi += 2)
            {
                tileCoords = new(currentTileCoordinates.x + Xi, currentTileCoordinates.y);
                if (FindTile(tileCoords, currentPFTile, _grid, openedList, out var PFTile))
                    surroundingTiles.Add(PFTile);
            }

            for (int Yi = -1; Yi <= 1; Yi += 2)
            {
                tileCoords = new(currentTileCoordinates.x, currentTileCoordinates.y + Yi);
                if (FindTile(tileCoords, currentPFTile, _grid, openedList, out var PFTile))
                    surroundingTiles.Add(PFTile);
            }
            return surroundingTiles;
        }


        private bool FindTile(Vector2Int tileCoords, PathFindingTile currentPFTile, IMoveGrid grid, Dictionary<Vector2Int, PathFindingTile> openedList, out PathFindingTile findingTile)
        {
            if (openedList.TryGetValue(new Vector2Int(tileCoords.x, tileCoords.y), out findingTile))
            {
            }
            else if (grid.TryGetTile(new Vector2Int(tileCoords.x, tileCoords.y), out IMovable tileData))
            {
                findingTile = new PathFindingTile(tileCoords, tileData.GetMovementDifficulty(), tileData.GetAvaibleDirections());
            }


            if (findingTile != null)
            {
                var currentDirections = currentPFTile.PathDirections;
                if (currentPFTile.coordinates.x - findingTile.coordinates.x == 0)
                {
                    if (currentDirections[TileDirections.North] && findingTile.PathDirections[TileDirections.South])
                    {
                        return true;
                    }
                    else if (currentDirections[TileDirections.South] && findingTile.PathDirections[TileDirections.North])
                    {
                        return true;
                    }
                }
                else if (currentPFTile.coordinates.y - findingTile.coordinates.y == 0)
                {
                   
                    if (currentDirections[TileDirections.East] && findingTile.PathDirections[TileDirections.West])
                    {
                        return true;
                    }
                    else if (currentDirections[TileDirections.West] && findingTile.PathDirections[TileDirections.East])
                    {
                        return true;
                    }
                }
            }

            findingTile = null;
            return false;

        }
    }
}
