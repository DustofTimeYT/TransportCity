using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindAlgo
{
    public class PathFindingTile
    {
        public PathFindingTile(Vector2Int coordinates, int movementDifficulty, Dictionary<TileDirections, bool> pathDirections)
        {
            this.coordinates = coordinates;
            this.movementDifficulty = movementDifficulty;
            PathDirections = pathDirections;
            pathLength = 0;
        }
        public int movementDifficulty { get; private set; }

        public Dictionary<TileDirections, bool> PathDirections { get; private set; }

        public Vector2Int coordinates { get; private set; }

        public Vector2Int previousTileCoords { get; private set; }
        public int tileWeight { get; private set; }
        public int pathLength { get; private set; }
        public int heuristicApproximation { get; private set; }

        /// <summary>
        /// Метод рассчета веса ячейки
        /// </summary>

        public void CalculateTileWeight()
        {
            tileWeight = pathLength + heuristicApproximation;
        }

        /// <summary>
        /// Метод попытки установки значения длины пути до этой ячейки из стартовой
        /// </summary>
        /// <param name="currentPathLenght">Новая длина пути, которую необходимо записать</param>
        /// <param name="previousTileCoords">Клетка, из которой пришли в эту клетку</param>
        /// <returns>Успешность попытки записи новой длины пути</returns>
        /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

        public bool TrySetPathLenght(Vector2Int previousTileCoords, int currentPathLenght)
        {
            if (currentPathLenght < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (currentPathLenght < pathLength || pathLength == 0)
            {
                this.pathLength = currentPathLenght;
                SetPreviousTile(previousTileCoords);
                return true;
            }
            return false;
        }

        private void SetPreviousTile(Vector2Int pCCoords)
        {
            if (Math.Abs(coordinates.x - pCCoords.x) + Math.Abs(coordinates.y - pCCoords.y) == 1)
            {
                this.previousTileCoords = pCCoords;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Метод записи эвристического приближения
        /// </summary>
        /// <param name="currentHeuristicApproximation">Рассчитаное значение эвристического приближения, которое необходимо записать</param>
        /// <exception cref="ArgumentOutOfRangeException">Значение не должно быть меньше нуля</exception>

        public void SetHeuristicApproximation(int currentHeuristicApproximation)
        {
            if (currentHeuristicApproximation < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            heuristicApproximation = currentHeuristicApproximation;
        }
    }
}