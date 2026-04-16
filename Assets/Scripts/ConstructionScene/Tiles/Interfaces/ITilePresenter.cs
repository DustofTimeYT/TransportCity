using AbsTile;
using System;
using UnityEngine;

public interface ITilePresenter
{
    public Vector2Int GetTilePosition();

    public Vector3 GetTile3DPosition();

    public string GetName();

    public void ChangeVisibility(bool isVisible);

    public void Delete();

    public abstract event Action<AbsTileModel> EventUpdateView;

    public event Action EventDeleteView;

    public event Action<bool> EventChangeVisibilityView;
}