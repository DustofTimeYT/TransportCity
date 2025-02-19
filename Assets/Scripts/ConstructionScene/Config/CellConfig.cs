using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellConfig", menuName = "ScriptableObjects/CellConfig", order = 1)]
public class CellConfig : ScriptableObject
{
    [SerializeField] private CellData _data;

    public CellData Data { get => _data; }
}
