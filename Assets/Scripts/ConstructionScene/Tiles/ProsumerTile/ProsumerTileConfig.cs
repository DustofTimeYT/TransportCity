using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные клетки по умолчанию
/// </summary>

[CreateAssetMenu(fileName = "ProsumerTileConfig", menuName = "ScriptableObjects/ProsumerTileConfig", order = 1)]
public class ProsumerTileConfig : AbsMoveTileConfig
{
    [field: SerializeField]
    public List<ProductType> ProducerProduct {  get; private set; }

    [field: SerializeField]
    public List<ProductType> ConsumerProduct { get; private set; }
}
