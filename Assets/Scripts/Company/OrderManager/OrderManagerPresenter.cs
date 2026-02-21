using Order;
using System;
using UnityEngine;

namespace OrderManager
{
    public class OrderManagerPresenter : IOrderManager
    {
        OrderManagerModel _model;
        IGarageManager _garageManager;
        BankPresenter _bankPresenter;

        public OrderManagerPresenter(IGarageManager garageManager, BankPresenter bankPresenter)
        {
            _model = new OrderManagerModel();
            _garageManager = garageManager;
            _bankPresenter = bankPresenter;
        }

        public IGarageManager GetGarageManager()
        {
            return _garageManager;
        }

        public void AddContract(IContract contract)
        {
            _model.Contracts.Add(contract);
            CreateOrder(contract);
        }
        
        private void CreateOrder(IContract contract)
        {
            OrderPresenter order = new OrderPresenter(contract);
            _model.Orders.Add(order.GetOrderNumber(), order);
            AddOrder?.Invoke(order);
        }

        private void UpdateOrder(IOrder order)
        {
            _model.Orders[order.GetOrderNumber()] = order;
            UpdateOrderLine?.Invoke(order);
        }
        private void UpdateOrder(RoutePresenter route)
        {
            var order = _model.Orders[route.GetOrder().GetOrderNumber()];
            order.SetDeliveredAmount(route.GetCargoAmount());
            UpdateOrder(order);
        }

        public void SubmitOrder(IOrder order)
        {
            UpdateOrder(order);
            var route = CreateRoute(order);
            Debug.Log($"Order #{order.GetOrderNumber()} was submit");
            AssignTransport(order, route);
        }

        public RoutePresenter CreateRoute(IOrder order)
        {   
            int cargoAmount = CalculateCargoAmount(order.GetTransport().GetMaxCapacity(), order.GetRemainingAmount());
            RoutePresenter route = new RoutePresenter(order, cargoAmount, this);
            _model.Routes.Add(route);
           return route;
        }

        public void AssignTransport(IOrder order, RoutePresenter route)
        {
            ITransport transport = order.GetTransport();
            transport.SetRoute(route);
            transport.BegineRoute();
        }

        public void FinishRoute(RoutePresenter route)
        {
            UpdateOrder(route);
            _model.Routes.Remove(route);
            var order = route.GetOrder();
            if (order.IsDelivered())
            {
                DeleteOrderLine?.Invoke(order);
                _model.Orders.Remove(order.GetOrderNumber());

                var contract = order.GetContract();

                _bankPresenter.DepositMoney(contract.GetMoneyAmount());
                Debug.Log($"Money has been credited to your account in the amount of {contract.GetMoneyAmount()}");
                _model.Contracts.Remove(contract);
                order.GetTransport().ReturnToBase();
                Debug.Log($"order {order.GetOrderNumber()} is delivered");

            }
            else
            {
                var newRoute = CreateRoute(order);
                AssignTransport(order, newRoute);
                Debug.Log("New Route");
            }
        }

        private int CalculateCargoAmount(int transportCapacity, int orderDeliveryAmount)
        {
            if( transportCapacity < orderDeliveryAmount )
            {
                return transportCapacity;
            }
            else
            {
                return orderDeliveryAmount;
            }
        }

        public event Action<IOrder> AddOrder;

        public event Action<IOrder> UpdateOrderLine;

        public event Action<IOrder> DeleteOrderLine;
    }
}
