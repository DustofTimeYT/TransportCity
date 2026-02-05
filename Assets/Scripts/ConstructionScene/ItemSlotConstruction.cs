using UnityEngine;

public class ItemSlotConstruction : MonoBehaviour
{
    private ConstructionSystem_V _cS_V;

    private AbsTileConfig _cellConfig;

    public void Init(ConstructionSystem_V cS_V, AbsTileConfig cellConfig)
    {
        _cellConfig = cellConfig;
        _cS_V = cS_V;
    }

    public void CreateItem()
    {
        _cS_V.StartPlacingTile(_cellConfig);
    }
}
