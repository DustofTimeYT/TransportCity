using UnityEngine;
using AbsTile;
/// <summary>
/// Представление клетки
/// </summary>

namespace AbsTile
{
    public abstract class AbsTileView : MonoBehaviour
    {
        protected AbsTilePresenter _presenter;

        public void Bind(AbsTilePresenter presenter, Vector2Int tileCoords)
        {
            _presenter = presenter;
            gameObject.transform.position = new Vector3(tileCoords.x, 0, tileCoords.y);

            Subscribe();
        }

        public void Bind(AbsTilePresenter presenter)
        {
            var cellCoords = presenter.GetTilePosition();
            _presenter = presenter;
            gameObject.transform.position = new Vector3(cellCoords.x, 0, cellCoords.y);
            this.name = $"{this.name} {cellCoords}";
            Subscribe();
        }

        protected void Subscribe()
        {
            _presenter.UpdateView += OnUpdateView;
            _presenter.DeleteView += OnDeleteView;
            _presenter.ChangeVisibilityView += OnChangeVisibilityView;
        }

        protected void Unsubscribe()
        {
            _presenter.UpdateView -= OnUpdateView;
            _presenter.DeleteView -= OnDeleteView;
            _presenter.ChangeVisibilityView -= OnChangeVisibilityView;
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