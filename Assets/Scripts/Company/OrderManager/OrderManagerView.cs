using Order;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OrderManagerView : MonoBehaviour
{
    [Inject]
    private OrderManagerPresenter _presenter;

    private List<OrderView> _orderLineViews;

    private int _lastLineIndex;

    [SerializeField] private int _lineAmount;

    [SerializeField]
    private GameObject _orderLinePref;

    [SerializeField]
    private Transform _orderLineContainer;

    private void Start()
    {
        _orderLineViews = new List<OrderView>();
        _lastLineIndex = 0;
    }

    private void OnEnable()
    {
        UpdateLines();
    }

    public void NextPage()
    {
        if (_lastLineIndex < _presenter.GetOrders().Count - _lineAmount)
        {
            _lastLineIndex = _lastLineIndex + _lineAmount;
        }
        UpdateLines();
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

    private void UpdateLines()
    {
        int index = 0;
        foreach (OrderPresenter orderLine in _presenter.GetOrders().Skip(_lastLineIndex).Take(_lineAmount))
        {
            UpdateLine(orderLine, index);
            index++;
        }
    }

    private void UpdateLine(OrderPresenter orderLine, int index)
    {
        if (_orderLineViews.Count < _lineAmount)
        {
            OrderView orderView = Instantiate(_orderLinePref, _orderLineContainer).GetComponent<OrderView>();
            if (orderView == null)
            {
                Debug.LogError("TransportSlotView cannot be NULL");
                return;
            }
            _orderLineViews.Add(orderView);
        }
        _orderLineViews[index].Bind(orderLine);

    }
} 
