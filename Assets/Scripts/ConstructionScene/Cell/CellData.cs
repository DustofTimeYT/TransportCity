using UnityEngine;

/// <summary>
///  ласс, позвол€ющий сохранить информацию о клетке
/// </summary>

[System.Serializable]
public class CellData
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private int _movementDifficulty;
    [SerializeField] private CellStateType _cellStateType;
    [SerializeField] private Vector2Int _cellCoordinates;

    public Vector2Int Size { get => _size; }

    public int MovementDifficulty { get => _movementDifficulty; }

    public CellStateType CellStateType { get => _cellStateType; }

    public Vector2Int CellCoordinates { get => _cellCoordinates; }
}  

