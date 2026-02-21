using Order;
using OrderLine;
using OrderManager;
using OrderMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderMenuPresenter : IMenuPresenter<OrderLinePresenter>
{
    OrderMenuModel _model;

    OrderManagerPresenter _orderManager;

    public OrderMenuPresenter(OrderManagerPresenter orderManager)
    {
        _model = new OrderMenuModel();
        _orderManager = orderManager;

        Subscribe();
    }

    private void Subscribe()
    {
        _orderManager.AddOrder += OnAddOrder;
        _orderManager.UpdateOrderLine += OnUpdateOrderLine;
        _orderManager.DeleteOrderLine += OnDeleteOrderLine;
    }

    private void OnDeleteOrderLine(IOrder order)
    {
        var orderLine = _model.OrderLines[order.GetOrderNumber()];
        _model.OrderLines.Remove(order.GetOrderNumber());
        UpdateView?.Invoke();
    }

    private void OnUpdateOrderLine(IOrder order)
    {
        _model.OrderLines[order.GetOrderNumber()].UpdateOrderData(order);
    }

    private void OnAddOrder(IOrder order)
    {
        CreateOrderLine(order);
        UpdateView?.Invoke();
    }

    private void CreateOrderLine(IOrder order)
    {
        OrderLinePresenter orderLine = new OrderLinePresenter(order, this);
        _model.OrderLines.Add(order.GetOrderNumber(),orderLine);
        UpdateView?.Invoke();
    }

    public IGarageManager GetGarageManager()
    {
        return _orderManager.GetGarageManager();
    }

    public List<OrderLinePresenter> GetLines()
    {
        return _model.OrderLines.Values.ToList();
    }

    public void SubmitOrder(IOrder order)
    {
        _orderManager.SubmitOrder(order);
    }

    public event Action UpdateView;
}
