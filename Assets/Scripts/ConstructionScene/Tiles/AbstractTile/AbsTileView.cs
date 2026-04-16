using UnityEngine;
using AbsTile;
/// <summary>
/// Представление клетки
/// </summary>

namespace AbsTile
{
    public abstract class AbsTileView : MonoBehaviour
    {
        protected ITilePresenter _presenter;

        public void Bind(ITilePresenter presenter)
        {
            _presenter = presenter;
            gameObject.transform.position = _presenter.GetTile3DPosition();
            this.name = $"{_presenter.GetName()} {_presenter.GetTilePosition()}";
            Subscribe();
        }

        protected void Subscribe()
        {
            _presenter.EventUpdateView += OnUpdateView;
            _presenter.EventDeleteView += OnDeleteView;
            _presenter.EventChangeVisibilityView += OnChangeVisibilityView;
        }

        protected void Unsubscribe()
        {
            _presenter.EventUpdateView -= OnUpdateView;
            _presenter.EventDeleteView -= OnDeleteView;
            _presenter.EventChangeVisibilityView -= OnChangeVisibilityView;
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

        protected void OnChangeVisibilityView(bool isVisble)
        {
            gameObject.SetActive(isVisble);
        }

        protected abstract void OnUpdateView(AbsTileModel cellModel);
    }
}