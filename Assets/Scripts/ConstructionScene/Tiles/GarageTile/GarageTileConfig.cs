using UnityEngine;

[CreateAssetMenu(fileName = "GarageTileConfig", menuName = "ScriptableObjects/GarageTileConfig", order = 1)]
public class GarageTileConfig : AbsMoveTileConfig
{
    [field: SerializeField]
    public int AmountParkingSlots { get; private set; }
}
