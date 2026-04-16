using AbsTile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbsMoveTile
{
    public abstract class AbsMoveTileModel : AbsTileModel
    {
        public int MovementDifficulty { get; protected set; }

        public Dictionary<TileDirections, bool> AvaibleDirections { get; protected set; }

        public AbsMoveTileModel(Vector2Int coords, int rotationAngle, AbsTileConfig config) : base(coords, rotationAngle, config)
        {
            AvaibleDirections = new();
            SetDefault(coords, rotationAngle, config as AbsMoveTileConfig);
        }

        private void SetDefault(Vector2Int coords, int rotationAngle, AbsMoveTileConfig config)
        {
            TrySetMovementDifficulty(config.MovementDifficulty);
            SetAvaibleDirections(rotationAngle, config);
        }

        public bool TrySetMovementDifficulty(int value)
        {
            if (value < 1)
                throw new ArgumentException("MovementDifficulty cannot be less than 1");

            MovementDifficulty = value;
            return true;
        }

        protected void SetAvaibleDirections(int rotationAngle, AbsMoveTileConfig config)
        {
            Queue<bool> directions = new();

            directions.Enqueue(config.North);
            directions.Enqueue(config.East);
            directions.Enqueue(config.South);
            directions.Enqueue(config.West);

            for (int angle = 0; angle != rotationAngle / 90; ++angle)
            {
                directions.Enqueue(directions.Dequeue());
                Debug.Log(angle);
            }

            var arr = directions.ToArray();
            Debug.Log($"config North = {config.North}, East = {config.East}, South = {config.South}, West = {config.West} ");
            Debug.Log($"queue North = {arr[0]}, East = {arr[1]}, South = {arr[2]}, West = {arr[3]} ");



            AvaibleDirections.Add(TileDirections.West, directions.Dequeue());
            AvaibleDirections.Add(TileDirections.South, directions.Dequeue());
            AvaibleDirections.Add(TileDirections.East, directions.Dequeue());
            AvaibleDirections.Add(TileDirections.North, directions.Dequeue());
        }

    }
}