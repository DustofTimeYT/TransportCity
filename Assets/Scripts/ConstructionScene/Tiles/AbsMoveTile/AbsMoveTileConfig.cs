using UnityEngine;

public abstract class AbsMoveTileConfig : AbsTileConfig
{
    [field: SerializeField]
    public int MovementDifficulty { get; private set; }

    [field: SerializeField]
    public bool North { get; private set; }

    [field: SerializeField]
    public bool East { get; private set; }

    [field: SerializeField]
    public bool South { get; private set; }

    [field: SerializeField]
    public bool West { get; private set; }
}