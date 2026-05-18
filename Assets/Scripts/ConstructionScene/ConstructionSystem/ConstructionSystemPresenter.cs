using Gird;
using ItemSlotConstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace ConstructionSystem
{
    public class ConstructionSystemPresenter : IMenuPresenter<ItemSlotConstructionPresenter>
    {
        private ConstructionSystemModel _model;

        private readonly IGrid _grid;

        private TilesConfig _tilesConfig;

        public ConstructionSystemPresenter(IGrid grid, TilesConfig tilesConfig)
        {
            _model = new();
            _grid = grid;
            _tilesConfig = tilesConfig;

            CreateItemLines();
        }

        public bool CheckAvaibleCoords(Vector2Int coords)
        {
            return _grid.CheckAvaibleCoords(coords);
        }

        public void ShowTile(Vector3 position)
        {
            ChangeVisibility(position, true);
        }

        public void HideTile(Vector3 position)
        {
            ChangeVisibility(position, false);
        }

        private void CreateItemLines()
        {
            foreach (var item in _tilesConfig.GetAllTiles())
            {
                _model.ItemSlotLines.Add(new(item, this));
            }
        }

        private void ChangeVisibility(Vector3 position3, bool isVisible)
        {
            Vector2Int position2Int = new Vector2Int(Mathf.RoundToInt(position3.x), Mathf.RoundToInt(position3.z));
            if (CheckAvaibleCoords(position2Int))
            {
                _grid.ChangeVisibility(position2Int, isVisible);
            }
        }

        public bool TryPlaceTile(Vector2Int coords, int rotationAngle, AbsTileConfig tileConfig)
        {
            if (_grid.TryReplaceTile(coords, rotationAngle, tileConfig, out ITilePresenter tile)) return true;
            return false;
        }

        public List<ItemSlotConstructionPresenter> GetLines()
        {
            return _model.ItemSlotLines;
        }

        public void SetSelectedTileConfig(AbsTileConfig tileConfig)
        {
            TileConfigSelected?.Invoke(tileConfig);
        }

        public event Action UpdateView;
        public event Action<AbsTileConfig> TileConfigSelected;
    }
}
