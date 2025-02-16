using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData
{
    private CellView cellView;
    private Vector2Int size = Vector2Int.one;
    public int movementDifficulty;

    public Vector2Int coordinates;

    public CellData(GameObject prefab, Vector2Int coordinates)
    {
        this.coordinates = coordinates;
        movementDifficulty = 1;
        cellView = GameObject.Instantiate(prefab, new Vector3(coordinates.x, 0, coordinates.y), new Quaternion()).GetComponent<CellView>();
        cellView.Init(this);
    }
}
