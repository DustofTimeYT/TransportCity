using AbsTile;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gird;

namespace Grid
{
    public class GridPresenter : IGrid
    {
        private GridModel _model;

        private Transform _gridGO;

        public GridPresenter(GridGenerator generator)
        {
            _gridGO = new GameObject("Grid").transform;
            var grid = generator.GenerateGridModel(_gridGO);
            _model = new GridModel(grid);
            Subscribe();
        }

        private void Subscribe()
        {

        }

        public void ChangeVisibility(Vector2Int coords, bool isVisible)
        {
            TryGetTile(coords, out AbsTilePresenter tile);
            tile.ChangeVisibility(isVisible);
        }

        public bool TryReplaceTile(Vector2Int coords, AbsTileConfig tileConfig, out AbsTilePresenter tile)
        {
            if (!TryGetTile(coords, out tile))
            {
                Debug.LogError("Вы находитесь за пределами поля");
                return false;
            }

            if (tile != null)
            {
                AbsTilePresenter newTile;
                switch (tileConfig.TileType)
                {
                    case TileType.Road:
                        newTile = new RoadTilePresenter(tile.GetTilePosition(), tileConfig, _gridGO);
                        break;

                    case TileType.Structure:
                        newTile = new StructureTilePresenter(tile.GetTilePosition(), tileConfig, _gridGO);
                        break;

                    default:
                        newTile = null;
                        Debug.LogError("Tile was created without Type");
                        break;
                }

                if (_model.TryReplaceTile(newTile))
                {
                    ReplaceTile?.Invoke(newTile);
                    tile.Delete();
                }
                else
                {
                    Debug.LogError("Tile was not replaced");
                    return false;
                }
             }

            return true;
        }

        public bool CheckAvaibleCoords(Vector2Int coords)
        {
            return TryGetTile(coords, out AbsTilePresenter tile);
        }

        public Dictionary<Vector2Int, AbsTilePresenter> GetAllTiles()
        {
           return _model.Tiles;
        }

        public bool TryGetTiles(IReadOnlyList<Vector2Int> tilesPos, out List<AbsTilePresenter> tiles)
        {
            tiles = new();

            foreach (Vector2Int tilePos in tilesPos)
            {
                if (!TryGetTile(tilePos, out AbsTilePresenter tile)) return false;
                tiles.Add(tile);
            }

            return true;
        }

        private bool TryGetCells(IReadOnlyList<Vector2Int> cellsPos, out List<IMovable> cells)
        {
            cells = new();

            foreach (Vector2Int cellPos in cellsPos)
            {
                if (!TryGetTile(cellPos, out IMovable cell)) return false;
                cells.Add(cell);
            }

            return true;
        }

        private bool TryGetTile(Vector2Int cellPos, out AbsTilePresenter cell)
        {
            cell = null;
            if (_model.Tiles.TryGetValue(cellPos, out var _structure))
            {
                cell = _structure;
                return true;
            }
            //if (_model.Roads.TryGetValue(cellPos, out var _road))
            //{
            //    //cell = _road;
            //    return true;
            //}
            //if (_model.Producers.TryGetValue(cellPos, out var _producer))
            //{
            //    //cell = _producer;
            //    Debug.LogError("Производитель не доделан");
            //    return true;
            //}
            //if (_model.Consumers.TryGetValue(cellPos, out var _consumer))
            //{
            //    //cell = _consumer;
            //    Debug.LogError("Потребитель не доделан");
            //    return true;
            //}
            return false;
        }

        public bool TryGetTile(Vector2Int cellPos, out IMovable cell)
        {
            //if (_gridModel.Roads.TryGetValue(cellPos, out var RCPcell))
            //{
            //    cell = RCPcell;
            //    return true;
            //}

            cell = null;
            return false;
        }

        public event Action<AbsTilePresenter> ReplaceTile;

    }
}
