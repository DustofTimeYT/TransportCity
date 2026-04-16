using System;
using UnityEngine;

/// <summary>
/// Класс, предоставляющий возможность взаимодействовать с данными
/// </summary>

namespace AbsTile
{
    public abstract class AbsTilePresenter<TileModel> : ITilePresenter where TileModel : AbsTileModel
    {
        protected TileModel _model;

        public AbsTilePresenter(Vector2Int coords, int rotationAngle, AbsTileConfig config, Transform parent)
        {
        }

        public Vector2Int GetTilePosition()
        {
            return _model.TileCoords;
        }

        public Vector3 GetTile3DPosition()
        {
            return new Vector3(_model.TileCoords.x, 0, _model.TileCoords.y);
        }

        public string GetName()
        {
            return _model.Name;
        }

        protected void InstantiateView(GameObject CellPref, int rotationAngle, Transform parent)
        {
            GameObject go = GameObject.Instantiate(CellPref, parent);
            go.transform.Rotate(0, rotationAngle, 0);
            var view = go.GetComponent<AbsTileView>();
            view.Bind(this);
        }

        public void ChangeVisibility(bool isVisible)
        {
            EventChangeVisibilityView?.Invoke(isVisible);
        }

        public void Delete()
        {
            EventDeleteView?.Invoke();
        }

        public void UpdateView(AbsTileModel model)
        {
            EventUpdateView?.Invoke(_model);
        }

        public event Action<AbsTileModel> EventUpdateView;

        public event Action EventDeleteView;

        public event Action<bool> EventChangeVisibilityView;
    }
}
