using UnityEngine;

/// <summary>
/// Данные сетки по умолчанию
/// </summary>

[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig", order = 1)]
public class GridConfig : ScriptableObject
{
    [field: SerializeField]
    public Vector2Int GridSize { get; private set; }
}
