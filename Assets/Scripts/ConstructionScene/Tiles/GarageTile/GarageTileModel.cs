using AbsTile;
using System.Collections.Generic;
using UnityEngine;

namespace GarageTile
{
    public class GarageTileModel : AbsTileModel
    {
        private static Dictionary<string, int> _garageNambers = new();

        private GarageTileConfig _config;

        public List<ITransport> Transports { get; private set; }

        public int AmountParkingSpots {  get; private set; }

        public int MovementDifficulty { get; private set; }


        public GarageTileModel(AbsTileConfig TileConfig, Vector2Int coords) : base(coords, TileConfig)
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
            Transports = new List<ITransport>();
        }
    }
}
