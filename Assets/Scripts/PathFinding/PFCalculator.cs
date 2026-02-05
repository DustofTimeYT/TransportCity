using UnityEngine;

namespace PathFindAlgo
{
    public class PFCalculator
    {
        private static int _pathCost = 10;

        /// <summary>
        /// Метод рассчета длины пути до выбранной клетки
        /// </summary>
        /// <param name="currentTilePathLenght">Длина пути до предшествующей клетки</param>
        /// <param name="movementDifficulty">Сложность перемещения по текущей клетке</param>
        /// <returns>Длину пути до переданной клетки</returns>

        public static int CalculatePathLenght(int currentTilePathLenght, int movementDifficulty)
        {
            return currentTilePathLenght + _pathCost * movementDifficulty;
        }

        /// <summary>
        /// Метод рассчета эвристического приближения с помощью манхэттенского расстояния 
        /// </summary>
        /// <param name="currentTile">Координаты (x, y) ячейки для которой рассчитывается приближение</param>
        /// <param name="endTile">Координаты (x, y) целевой ячейки</param>
        /// <returns>Эвристическое приближение текущей клетки</returns>

        public static int CalculateHeuristicApproximation(Vector2Int currentTile, Vector2Int endTile)
        {
            return (Mathf.Abs(currentTile.x - endTile.x) + Mathf.Abs(currentTile.y - endTile.y)) * _pathCost;
        }
    }
}
