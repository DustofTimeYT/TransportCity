using AbsTile;
using UnityEngine;

namespace GarageTile
{
    public class GarageTileModel : AbsTileModel
    {
        private GarageTileConfig _config;

        public int MovementDifficulty { get; private set; }

        public GarageTileModel(AbsTileConfig TileConfig, Vector2Int coords) : base(coords)
        {
            _config = ValidateConfigType<GarageTileConfig>(TileConfig);

            SetDefault();
        }

        public void TrySetMovementDifficulty(int value)
        {
            if (value < 1)
                return;
            MovementDifficulty = value;
        }

        public override void SetDefault()
        {
            MovementDifficulty = _config.MovementDifficulty;
        }
    }
}
