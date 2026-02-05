using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "GarageTileConfig", menuName = "ScriptableObjects/GarageTileConfig", order = 1)]
public class GarageTileConfig : AbsTileConfig
{
    [field: SerializeField]
    public int MovementDifficulty { get; private set; }
}
