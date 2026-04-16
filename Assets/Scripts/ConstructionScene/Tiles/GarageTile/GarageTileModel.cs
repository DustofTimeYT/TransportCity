using AbsMoveTile;
using System.Collections.Generic;
using UnityEngine;

namespace GarageTile
{
    public class GarageTileModel : AbsMoveTileModel
    {
        private GarageTileConfig _config;

        public List<ITransport> Transports { get; private set; }

        public int AmountParkingSpots {  get; private set; }


        public GarageTileModel(AbsTileConfig TileConfig, int rotationAngle, Vector2Int coords) : base(coords, rotationAngle, TileConfig)
        {
            _config = ValidateConfigType<GarageTileConfig>(TileConfig);

            SetDefault();
        }

        private void SetDefault()
        {
            Transports = new List<ITransport>();
        }
    }
}
