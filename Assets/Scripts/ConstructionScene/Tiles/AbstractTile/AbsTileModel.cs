using System;
using AbsTile;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

namespace AbsTile
{
    public abstract class AbsTileModel
    {
        public Vector2Int TileCoords { get; protected set; }

        protected AbsTileModel(Vector2Int coords)
        {
            TileCoords = coords;
        }

        protected T ValidateConfigType<T>(AbsTileConfig config) where T : AbsTileConfig
        {
            return config as T ?? throw new ArgumentException(
                $"Expected {typeof(T).Name}, but got {config?.GetType().Name}",
                nameof(config));
        }

        public abstract void SetDefault();
    }
}
