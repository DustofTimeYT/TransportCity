using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGarageGrid 
{
    public bool TryGetTile(Vector2Int tilePos, out IGarage tile);

    public Dictionary<Vector2Int, IGarage> GetGarages();

    public event Action UpdateLayer;
}