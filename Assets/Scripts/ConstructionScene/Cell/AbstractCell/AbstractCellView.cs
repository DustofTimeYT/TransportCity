using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Представление клетки
/// </summary>

public abstract class AbstractCellView : MonoBehaviour
{
    protected AbstractCellPresenter _presenter;

    public void Bind(AbstractCellPresenter presenter, Vector2Int cellCoordinates)
    {
        _presenter = presenter;
        gameObject.transform.position = new Vector3(cellCoordinates.x, 0 , cellCoordinates.y);

        Subscribe();
        //_presenter.UpdateView_();
    }

    public void Bind(AbstractCellPresenter presenter)
    {
        var cellCoords = presenter.GetCellPosition();
        _presenter = presenter;
        gameObject.transform.position = new Vector3(cellCoords.x, 0, cellCoords.y);
        this.name = $"{this.name} {cellCoords}";
        Subscribe();
    }

    protected void Subscribe()
    {
        _presenter.UpdateView += OnUpdateView;
        _presenter.DeleteView += OnDeleteView;
    }

    protected void Unsubscribe()
    {
        _presenter.UpdateView -= OnUpdateView;
        _presenter.DeleteView -= OnDeleteView;
    }

    /// <summary>
    /// Получение доступных действий с клеткой
    /// </summary>
    /// <returns>список доступных действий с клеткой</returns>

    public List<CellActionData> GetActionData()
    {
        return _presenter.GetActionData();
    }


    /// <summary>
    /// Метод перерисовки данных
    /// </summary>
    /// <param name="cellModel">Данные клетки</param>

    protected void OnDeleteView()
    {
        Unsubscribe();
        Destroy(gameObject);
    }

    protected abstract void OnUpdateView(AbstractCellModel cellModel);
}
