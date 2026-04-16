
using System.Collections.Generic;
using Unity.VisualScripting;

public interface IMovable
{
    public int GetMovementDifficulty();

    public Dictionary<TileDirections, bool> GetAvaibleDirections();
}
