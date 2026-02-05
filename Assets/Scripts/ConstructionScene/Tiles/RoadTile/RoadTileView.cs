using UnityEngine;
using AbsTile;

/// <summary>
/// Представление клетки
/// </summary>

public class RoadTileView : AbsTileView
{
    [SerializeField] private Renderer _mainRenderer;

    [SerializeField] private Material _highlightColor;

    /// <summary>
    /// Метод перерисовки данных
    /// </summary>
    /// <param name="cellModel">Данные клетки</param>

    protected override void OnUpdateView(AbsTileModel AbsTileModel)
    {
        RoadTileModel TileModel = (RoadTileModel)AbsTileModel;
    }
}
