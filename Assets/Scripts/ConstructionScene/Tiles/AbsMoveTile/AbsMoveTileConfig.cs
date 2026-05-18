using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsMoveTileConfig : AbsTileConfig
{
    [field: SerializeField]
    public int MovementDifficulty { get; private set; }

    public bool SeparateDirections;

    [SerializeField, SerializedDictionary("Direction", "Avaible")]
    private SerializedDictionary<TileDirections, bool> IncomingDirections;

    
    [SerializeField, SerializedDictionary("Direction", "Avaible")]
    private SerializedDictionary<TileDirections, bool> OutgoingDirections;

    public Dictionary<TileDirections, bool> GetIncomingDirections()
    {
        return IncomingDirections;
    }

    public Dictionary<TileDirections, bool> GetOutgoingDirections()
    {
        if(SeparateDirections)
            return OutgoingDirections;
        else
            return IncomingDirections;
    }
}