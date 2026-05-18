
using System.Collections.Generic;
using Unity.VisualScripting;

public interface IMovable
{
    public int GetMovementDifficulty();

    public Dictionary<TileDirections, bool> GetIncomingDirections();

    public Dictionary<TileDirections, bool> GetOutgoingDirections();
}
