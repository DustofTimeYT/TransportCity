using System.Collections.Generic;
using UnityEngine;

public interface IGarage : IOption
{
    public Vector2Int GetTilePosition();

    public List<ITransport> GetTransports();

    public bool FindTransport(string name, out ITransport transport);

    public void AddTransport(ITransport transport);
}
