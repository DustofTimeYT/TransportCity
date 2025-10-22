using UnityEngine;

/// <summary>
/// Данные клетки по умолчанию
/// </summary>

[CreateAssetMenu(fileName = "CellConfig", menuName = "ScriptableObjects/CellConfig", order = 1)]
public class CellConfig : ScriptableObject
{
    [SerializeField] private CellData _data;

    [field: SerializeField]
    public GameObject CellPref { get; private set; }

    [field: SerializeField]
    public CellType CellType { get; private set; }

    public CellData Data { get => _data; }
}
