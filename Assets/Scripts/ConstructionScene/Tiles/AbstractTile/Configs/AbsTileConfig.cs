using System;
using UnityEngine;

/// <summary>
/// Данные клетки по умолчанию
/// </summary>

public abstract class AbsTileConfig : ScriptableObject
{
    [field: SerializeField]
    public GameObject TilePref { get; private set; }

    [field: SerializeField]
    public TileType TileType { get; private set; }
}
