using AbsTile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbsMoveTile
{
    public abstract class AbsMoveTileModel : AbsTileModel
    {
        public int MovementDifficulty { get; protected set; }

        public Dictionary<TileDirections, bool> IncomingDirections { get; protected set; }

        public Dictionary<TileDirections, bool> OutgoingDirections { get; protected set; }

        public AbsMoveTileModel(Vector2Int coords, int rotationAngle, AbsTileConfig config) : base(coords, rotationAngle, config)
        {
            SetDefault(coords, rotationAngle, config as AbsMoveTileConfig);
        }

        private void SetDefault(Vector2Int coords, int rotationAngle, AbsMoveTileConfig config)
        {
            TrySetMovementDifficulty(config.MovementDifficulty);
            IncomingDirections = SetDirections(rotationAngle, config.GetIncomingDirections());
            OutgoingDirections = SetDirections(rotationAngle, config.GetOutgoingDirections());
        }

        public bool TrySetMovementDifficulty(int value)
        {
            if (value < 1)
                throw new ArgumentException("MovementDifficulty cannot be less than 1");

            MovementDifficulty = value;
            return true;
        }

        protected Dictionary<TileDirections, bool> SetDirections(int rotationAngle, Dictionary<TileDirections, bool> defaultDirections)
        {
            Queue<bool> directions = new();

            directions.Enqueue(defaultDirections[TileDirections.North]);
            directions.Enqueue(defaultDirections[TileDirections.East]);
            directions.Enqueue(defaultDirections[TileDirections.South]);
            directions.Enqueue(defaultDirections[TileDirections.West]);

            for (int angle = 0; angle != rotationAngle / 90; ++angle)
            {
                directions.Enqueue(directions.Dequeue());
                Debug.Log(angle);
            }

            var arr = directions.ToArray();
            //Debug.Log($"config North = {config.North}, East = {config.East}, South = {config.South}, West = {config.West} ");
            //Debug.Log($"queue North = {arr[0]}, East = {arr[1]}, South = {arr[2]}, West = {arr[3]} ");

            Dictionary<TileDirections, bool> avaibleDirections = new()
            {
                { TileDirections.West, directions.Dequeue() },
                { TileDirections.South, directions.Dequeue() },
                { TileDirections.East, directions.Dequeue() },
                { TileDirections.North, directions.Dequeue() }
            };

            return avaibleDirections;
        }
    }
}