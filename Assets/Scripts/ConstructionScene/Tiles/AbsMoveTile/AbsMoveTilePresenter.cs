using AbsTile;
using System.Collections.Generic;
using UnityEngine;

namespace AbsMoveTile
{
    public abstract class AbsMoveTilePresenter<TileModel> : AbsTilePresenter<TileModel>, IMovable where TileModel : AbsMoveTileModel
    {
        protected AbsMoveTilePresenter(Vector2Int coords, int rotationAngle, AbsTileConfig config, Transform parent) : base(coords, rotationAngle, config, parent)
        {
        }

        public int GetMovementDifficulty()
        {
            return _model.MovementDifficulty;
        }

        public void SetMovementDifficulty(int value)
        {
            _model.TrySetMovementDifficulty(value);
            UpdateView(_model);
        }

        public Dictionary<TileDirections, bool> GetIncomingDirections()
        {
            return _model.IncomingDirections;
        }

        public Dictionary<TileDirections, bool> GetOutgoingDirections()
        {
            return _model.OutgoingDirections;
        }
    }
}