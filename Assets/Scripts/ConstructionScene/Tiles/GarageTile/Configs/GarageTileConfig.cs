using UnityEngine;

[CreateAssetMenu(fileName = "GarageTileConfig", menuName = "ScriptableObjects/GarageTileConfig", order = 1)]
public class GarageTileConfig : AbsTileConfig
{
    [field: SerializeField]
    public int AmountParkingSlots { get; private set; }

    [field: SerializeField]
    public int MovementDifficulty { get; private set; }
}
