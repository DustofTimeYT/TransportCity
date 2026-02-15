using AbsTile;
using System.Collections.Generic;
using UnityEngine;

namespace ProsumerTile
{
    public class ProsumerTileModel : AbsTileModel
    {
        private ProsumerTileConfig _config;

        public List<ProductType> ProducerProduct { get; private set; }

        public List<ProductType> ConsumerProduct { get; private set; }

        public ProsumerTileModel(AbsTileConfig TileConfig, Vector2Int coords) : base(coords, TileConfig)
        {
            _config = ValidateConfigType<ProsumerTileConfig>(TileConfig);
            SetDefault();
        }

        public override void SetDefault()
        {
            ProducerProduct = _config.ProducerProduct;
            ConsumerProduct = _config.ConsumerProduct;
        }
    }
}