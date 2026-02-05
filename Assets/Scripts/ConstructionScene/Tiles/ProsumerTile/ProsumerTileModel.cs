using AbsTile;
using UnityEngine;

namespace ProsumerTile
{
    public class ProsumerTileModel : AbsTileModel
    {
        private ProsumerTileConfig _tileConfig;
        public ProsumerTileModel(AbsTileConfig TileConfig, Vector2Int coords) : base(coords)
        {
            _tileConfig = ValidateConfigType<ProsumerTileConfig>(TileConfig);
        }

        public override void SetDefault()
        {
        }
    }
}