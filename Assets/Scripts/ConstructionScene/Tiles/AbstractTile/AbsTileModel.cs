using AbsTile;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

namespace AbsTile
{
    public abstract class AbsTileModel
    {
        private static Dictionary<string, int> _tileNames = new();

        public string Name { get; private set; }

        public Vector2Int TileCoords { get; private set; }

        protected AbsTileModel(Vector2Int coords, AbsTileConfig config)
        {
            TileCoords = coords;
            SetValidName(config);
        }

        protected T ValidateConfigType<T>(AbsTileConfig config) where T : AbsTileConfig
        {
            return config as T ?? throw new ArgumentException(
                $"Expected {typeof(T).Name}, but got {config?.GetType().Name}",
                nameof(config));
        }

        protected void SetValidName(AbsTileConfig config)
        {
            if (_tileNames.ContainsKey(config.Name))
            {
                if (_tileNames.TryGetValue(config.Name, out int number))
                {
                    Name = $"{config.Name} {number}";
                    _tileNames.Remove(config.Name);
                    _tileNames.Add(config.Name, number + 1);

                }
                else
                {
                    Debug.LogError($"Couldn't assign a name to {config.Name}");
                    return;
                }
            }
            else
            {
                Name = config.Name;
                _tileNames.Add(config.Name, 1);
            }
        }

        public abstract void SetDefault();
    }
}
