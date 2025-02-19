using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig", order = 1)]
public class GridConfig : ScriptableObject
{
    [SerializeField] private Vector2Int _gridSize;

    public Vector2Int GridSize { get => _gridSize; }
}
