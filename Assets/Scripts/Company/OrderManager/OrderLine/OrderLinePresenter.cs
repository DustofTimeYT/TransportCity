using System;
using System.Collections.Generic;
using UnityEngine;

namespace OrderLine
{
    public class OrderLinePresenter
    {
        private OrderLineModel _model;
        private OrderMenuPresenter _orderMenu;

        public OrderLinePresenter(IOrder order, OrderMenuPresenter orderMenu)
        {
            _model = new OrderLineModel(order);
            _orderMenu = orderMenu; 
            Subscribe();
        }

        private void Subscribe()
        {
            _orderMenu.GetGarageManager().RefreshGarages += OnRefreshGarages;
        }

        private void OnRefreshGarages()
        {
            UpdateView?.Invoke();
        }

        public int GetOrderNumber()
        {
            return _model.Order.GetOrderNumber();
        }

        public bool IsInProgress()
        {
            return _model.IsInProgress;
        }

        public string GetDeliveredItem()
        {
            return _model.Order.GetDeliveredItem().ToString();
        }

        public int GetDeliveredAmount()
        {
            return _model.Order.GetDeliveredAmount();
        }

        public int GetTotalAmount()
        {
            return _model.Order.GetTotalAmount();
        }

        public void SetGarage(string garageName)
        {
            if (garageName != null)
            {
                _orderMenu.GetGarageManager().FindGarage(garageName, out var garage);
                _model.SetGarage(garage);
            }
            else
            {
                _model.SetGarage(null);
            }
        }

        public void SetTransport(string transportName)
        {
            if (transportName != null)
            {
                _model.Garage.FindTransport(transportName, out var transport);
                _model.Order.SetTransport(transport);
            }
            else
            {
                _model.Order.SetTransport(null);
            }
        }

        public List<string> GetGarages()
        {
            List<string> garagesName = new List<string>();
            if (_orderMenu.GetGarageManager().GetGarages() != null)
            {
                foreach (IGarage garage in _orderMenu.GetGarageManager().GetGarages())
                {
                    garagesName.Add(garage.GetName());
                }
            }

            return garagesName;
        }

        public List<string> GetTransport()
        {
            List<string> transportsName = new List<string>();
            if (_model.Garage != null)
            {
                foreach (ITransport transport in _model.Garage.GetTransports())
                {
                    transportsName.Add(transport.GetName());
                }
            }

            return transportsName;
        }

        public void SubmitOrder()
        {
            _orderMenu.SubmitOrder(_model.Order);
            _model.SetInProgress();
        }

        public void UpdateOrderData(IOrder order)
        {
            _model.SetOrder(order);
            UpdateView?.Invoke();
        }

        public event Action UpdateView;
    }
}
