using Route;
using UnityEngine;

public class RoutePresenter
{
    private RouteModel _model;

    private IOrderManager _orderManager;

    public RoutePresenter(IOrder order, int cargoAmount, IOrderManager orderManager)
    {
        _model = new RouteModel(order, cargoAmount);
        _orderManager = orderManager;
    }

    public Vector2Int GetLoadPosition()
    {
        return  _model.LoadPosition;
    }
    public Vector2Int GetUnloadPosition()
    {
        return _model.UnloadPosition;
    }

    public RouteStatus GetRouteStatus()
    {
        return _model.Status;
    }

    public void SetRouteStatus(RouteStatus status)
    {
        _model.SetStatus(status);
    }

    public IProducer GetProducer()
    {
        return _model.Order.GetContract().GetProducer();
    }

    public IConsumer GetConsumer()
    {
        return _model.Order.GetContract().GetConsumer();
    }

    public ProductType GetProduct()
    {
        return _model.DeliveryItem;
    }

    public int GetCargoAmount()
    {
        return _model.CargoAmount;
    }

    public IOrder GetOrder()
    {
        return _model.Order;
    }

    public void RouteStarted()
    {
        _model.SetStatus(RouteStatus.Start);
    }

    public void RouteIsOver()
    {
        _model.SetStatus(RouteStatus.End);
        _orderManager.FinishRoute(this);
    }
}