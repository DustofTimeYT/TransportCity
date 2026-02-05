using AbsTile;
using UnityEngine;

/// <summary>
/// Класс, хранящий информацио о клетке во время сессии
/// </summary>

public class RoadTileModel : AbsTileModel
{
    private RoadTileConfig _config;

    public int MovementDifficulty { get; private set; }

    public RoadTileModel(AbsTileConfig config, Vector2Int coords) : base(coords)
    {
        _config = ValidateConfigType<RoadTileConfig>(config);

        SetDefault();
    }

    public void  TrySetMovementDifficulty(int value)
    {
        if (value < 1)
            return;
        MovementDifficulty = value;
    }

    public override void SetDefault()
    {
        MovementDifficulty = _config.MovementDifficulty;
    }
}
