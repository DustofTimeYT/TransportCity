using AbsMoveTile;
using System.Collections.Generic;
using UnityEngine;

namespace ProsumerTile
{
    public class ProsumerTileModel : AbsMoveTileModel
    {
        private ProsumerTileConfig _config;

        public List<ProductType> ProducerProduct { get; private set; }

        public List<ProductType> ConsumerProduct { get; private set; }

        public ProsumerTileModel(AbsTileConfig TileConfig, int rotationAngle, Vector2Int coords) : base(coords, rotationAngle, TileConfig)
        {
            _config = ValidateConfigType<ProsumerTileConfig>(TileConfig);
            SetDefault();
        }

        private void SetDefault()
        {
            ProducerProduct = _config.ProducerProduct;
            ConsumerProduct = _config.ConsumerProduct;
        }
    }
}