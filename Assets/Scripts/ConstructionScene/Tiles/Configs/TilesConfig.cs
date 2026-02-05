using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TilesConfig", menuName = "ScriptableObjects/TilesConfig", order = 1)]
public class TilesConfig : ScriptableObject
{
    [field: SerializeField]
    public List<AbsTileConfig> DefaultTiles { get; private set; }

    [field: SerializeField]
    public List<RoadTileConfig> RoadTiles { get; private set; }

    [field: SerializeField]
    public List<AbsTileConfig> StructureTiles { get; private set; }

    [field: SerializeField]
    public List<AbsTileConfig> ProsumerTiles { get; private set; }

    public List<AbsTileConfig> GetAllTiles()
    {
        List <AbsTileConfig> Tiles = new();

        Tiles.AddRange(GetDefaultTiles());
        Tiles.AddRange(GetRoadTiles());
        Tiles.AddRange(GetStructureTiles());
        Tiles.AddRange(GetProsumerTiles());

        return Tiles;
    }

    public List<AbsTileConfig> GetDefaultTiles()
    {
        List<AbsTileConfig> Tiles = new();

        foreach (var tile in DefaultTiles)
        {
            Tiles.Add(tile);
        }

        return Tiles;
    }

    public List<AbsTileConfig> GetRoadTiles()
    {
        List<AbsTileConfig> Tiles = new();

        foreach (var tile in RoadTiles)
        {
            Tiles.Add(tile);
        }

        return Tiles;
    }

    public List<AbsTileConfig> GetStructureTiles()
    {
        List<AbsTileConfig> Tiles = new();

        foreach (var tile in StructureTiles)
        {
            Tiles.Add(tile);
        }

        return Tiles;
    }

    public List<AbsTileConfig> GetProsumerTiles()
    {
        List<AbsTileConfig> Tiles = new();

        foreach (var tile in ProsumerTiles)
        {
            Tiles.Add(tile);
        }

        return Tiles;
    }
}
