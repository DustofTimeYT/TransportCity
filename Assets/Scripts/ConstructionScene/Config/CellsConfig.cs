using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsConfig", menuName = "ScriptableObjects/CellsConfig", order = 1)]
public class CellsConfig : ScriptableObject
{
    [field: SerializeField]
    public List<CellConfig> Cells { get; private set; }
}
