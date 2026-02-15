using Order;
using OrderManager;
using System.Collections.Generic;

public class OrderManagerPresenter : IOrderManager
{
    OrderManagerModel _model;

    public OrderManagerPresenter(OrderManagerModel model)
    {
        _model = model;
    }

    public void AddContract(IContract contract)
    {
        _model.Contracts.Add(contract);
        CreateOrder(contract);
    }

    private void CreateOrder(IContract contract)
    {
        OrderPresenter order = new OrderPresenter(contract);
        _model.Orders.Add(order);
    }

    public List<OrderPresenter> GetOrders()
    {
        return _model.Orders;
    }
}
