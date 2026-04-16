using UnityEngine;
using AbsTile;

namespace StructureTile
{
    public class StructureTileModel : AbsTileModel
    {
        private StructureTileConfig _config;

        public StructureTileModel(AbsTileConfig config, int rotationAngle, Vector2Int coords) : base(coords, rotationAngle, config)
        {
            _config = ValidateConfigType<StructureTileConfig>(config);
        }
    }
}
