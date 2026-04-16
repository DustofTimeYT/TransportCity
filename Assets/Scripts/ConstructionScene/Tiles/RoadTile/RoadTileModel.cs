using AbsMoveTile;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

public class RoadTileModel : AbsMoveTileModel
{
    private RoadTileConfig _config;

    public RoadTileModel(AbsMoveTileConfig config, int rotationAngle, Vector2Int coords) : base(coords, rotationAngle, config)
    {
        _config = ValidateConfigType<RoadTileConfig>(config);
        SetDefault();
    }

    private void SetDefault()
    {
        MovementDifficulty = _config.MovementDifficulty;
    }
}
