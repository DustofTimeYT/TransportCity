using System;
using AbsTile;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

public abstract class AbsTilePresenter
{
    public abstract Vector2Int GetTilePosition();

    protected void InstantiateView(GameObject CellPref, Transform parent)
    {
        var view = GameObject.Instantiate(CellPref, parent).GetComponent<AbsTileView>();
        view.Bind(this);
    }

    public void ChangeVisibility(bool isVisible)
    {
        ChangeVisibilityView?.Invoke(isVisible);
    }

    public void Delete()
    {
        DeleteView?.Invoke();
    }

    public abstract event Action<AbsTileModel> UpdateView;

    public event Action DeleteView;

    public event Action<bool> ChangeVisibilityView;
}
