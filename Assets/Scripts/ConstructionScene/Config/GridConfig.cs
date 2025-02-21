using UnityEngine;

/// <summary>
/// Данные сетки по умолчанию
/// </summary>

[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig", order = 1)]
public class GridConfig : ScriptableObject
{
    [SerializeField] private Vector2Int _gridSize;

    public Vector2Int GridSize { get => _gridSize; }
}
