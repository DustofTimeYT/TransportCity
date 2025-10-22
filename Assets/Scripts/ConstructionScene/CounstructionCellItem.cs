using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CounstructionCellItem : MonoBehaviour
{
    private ConstructionSystem_V _cS_V;

    private CellConfig _cellConfig;

    public void Init(ConstructionSystem_V cS_V, CellConfig cellConfig)
    {
        _cellConfig = cellConfig;
        _cS_V = cS_V;
    }

    public void CreateItem()
    {
        _cS_V.StartPlacingConstruction(_cellConfig);
    }
}
