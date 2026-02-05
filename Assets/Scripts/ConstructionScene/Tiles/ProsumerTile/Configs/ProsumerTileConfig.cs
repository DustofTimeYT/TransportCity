using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные клетки по умолчанию
/// </summary>

[CreateAssetMenu(fileName = "ProsumerTileConfig", menuName = "ScriptableObjects/ProsumerTileConfig", order = 1)]
public class ProsumerTileConfig : AbsTileConfig
{
    [field: SerializeField]
    public List<Product> ProducerProduct {  get; private set; }
}
