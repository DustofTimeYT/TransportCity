using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AbsMenu
{
    public enum OrientedLayout
    {
        Vertical,
        Horizontal
    }

    public abstract class AbsMenuView<MenuPresenter, LinePresenter, LineView> : MonoBehaviour where MenuPresenter : IMenuPresenter<LinePresenter> where LineView : IView<LinePresenter>
    {

        [SerializeField]
        protected OrientedLayout _orientedLayout = OrientedLayout.Vertical;

        [SerializeField]
        protected int _lineAmount;

        [SerializeField]
        protected GameObject _linePref;

        [SerializeField]
        protected Transform _linesContainer;

        protected int _lastLineIndex;

        [Inject]
        protected MenuPresenter _presenter;

        protected List<LineView> _lineViews;

        protected virtual void Awake()
        {
            Init();
            OnUpdateView();
        }

        protected virtual void Init()
        {
            _lineViews = new List<LineView>();
            _lastLineIndex = 0;

            //_lineAmount = _orientedLayout == OrientedLayout.Vertical? CalculateLineAmount() : _lineAmount;

            Subscribe();
        }

        private int CalculateLineAmount()
        {
            float containerHeight = _linesContainer.gameObject.GetComponent<RectTransform>().rect.height;
            float lineHeight = _linePref.GetComponent<RectTransform>().rect.height;

            return Mathf.FloorToInt(containerHeight/lineHeight);
        }

        protected virtual void Subscribe()
        {
            _presenter.UpdateView += OnUpdateView;
            Debug.Log($"{this.GetType()} was subscribed");
        }

        protected virtual void OnUpdateView()
        {
            UpdateLines();
        }

        public void NextPage()
        {
            if (_lastLineIndex < _presenter.GetLines().Count - _lineAmount)
            {
                _lastLineIndex = _lastLineIndex + _lineAmount;
                UpdateLines();
            }
        }
        public void PreviousPage()
        {
            _lastLineIndex = _lastLineIndex - _lineAmount;
            if (_lastLineIndex < 0)
            {
                _lastLineIndex = 0;
            }
            
            UpdateLines();
        }

        protected void UpdateLines()
        {
            int index = 0;

            foreach (LinePresenter linePresenter in _presenter.GetLines().Skip(_lastLineIndex).Take(_lineAmount))
            {
                UpdateLine(linePresenter, index);
                index++;
            }

            for (int i = index; i < _lineViews.Count; i++)
            {
                _lineViews[i].Hide();
            }
        }

        protected void UpdateLine(LinePresenter linePresenter, int index)
        {
            if (_lineViews.Count <= index)
            {
                LineView lineView = Instantiate(_linePref, _linesContainer).GetComponent<LineView>();
                if (lineView == null)
                {
                    Debug.LogError($"{lineView.GetType()} cannot be NULL");
                    return;
                }
                _lineViews.Add(lineView);
            }

            _lineViews[index].Show();
            _lineViews[index].Bind(linePresenter);

        }
    }
}