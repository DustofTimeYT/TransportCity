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

    [field: SerializeField]
    public List<GarageTileConfig> GarageTiles { get; private set; }

    public List<AbsTileConfig> GetAllTiles()
    {
        List <AbsTileConfig> Tiles = new();

        Tiles.AddRange(GetDefaultTiles());
        Tiles.AddRange(GetRoadTiles());
        Tiles.AddRange(GetStructureTiles());
        Tiles.AddRange(GetProsumerTiles());
        Tiles.AddRange(GetGarageTiles());

        return Tiles;
    }

    private List<AbsTileConfig> ToAbsTileConfig<T>(List<T> tiles) where T : AbsTileConfig
    {
        List<AbsTileConfig> newTiles = new();

        foreach (var tile in tiles)
        {
            newTiles.Add(tile);
        }

        return newTiles;
    }

    public List<AbsTileConfig> GetDefaultTiles()
    {
        return DefaultTiles;
    }

    public List<AbsTileConfig> GetRoadTiles()
    {
        return ToAbsTileConfig(RoadTiles);
    }

    public List<AbsTileConfig> GetStructureTiles()
    {
        return StructureTiles;

    }

    public List<AbsTileConfig> GetProsumerTiles()
    {
        return ToAbsTileConfig(ProsumerTiles);
    }

    public List<AbsTileConfig> GetGarageTiles()
    {
        return ToAbsTileConfig(GarageTiles);
    }
}
