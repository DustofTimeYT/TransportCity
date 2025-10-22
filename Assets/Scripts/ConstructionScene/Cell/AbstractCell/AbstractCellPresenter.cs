using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

public abstract class AbstractCellPresenter
{
    protected UIEventBus _UIEventBus;

    public abstract Vector2Int GetCellPosition();

    public abstract List<CellActionData> GetActionData();

    protected void InstantiateView(GameObject CellPref)
    {
        var view = GameObject.Instantiate(CellPref).GetComponent<AbstractCellView>();
        view.Bind(this);
    }

    public void Delete()
    {
        DeleteView?.Invoke();
    }

    public abstract event Action<AbstractCellModel> UpdateView;

    public event Action DeleteView;
}
