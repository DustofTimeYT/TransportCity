using System;
using UnityEngine;
using AbsTile;

namespace StructureTile
{
    public class StructureTileModel : AbsTileModel
    {
        private StructureTileConfig _config;

        public StructureTileModel(AbsTileConfig config, Vector2Int coords) : base(coords, config)
        {
            _config = ValidateConfigType<StructureTileConfig>(config);
        }

        public override void SetDefault()
        {
        }
    }
}
