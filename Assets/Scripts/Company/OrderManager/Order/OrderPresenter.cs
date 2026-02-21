namespace Order
{
    public class OrderPresenter : IOrder
    {
        private OrderModel _model;

        public OrderPresenter(IContract contract)
        {
            _model = new OrderModel(contract);
        }

        public IContract GetContract()
        {
            return _model.Contract;
        }

        public int GetOrderNumber()
        {
            return _model.OrderNumber;
        }

        public void SetDeliveredAmount(int amount)
        {
            _model.TrySetDeliveredAmount(amount);
        }

        public int GetDeliveredAmount()
        {
            return _model.DeliveredAmount;
        }

        public int GetRemainingAmount()
        {
            return GetTotalAmount() - GetDeliveredAmount();
        }

        public int GetTotalAmount()
        {
            return _model.Contract.GetDeliveryAmount();
        }

        public ITransport GetTransport()
        {
            return _model.Transport;
        }

        public ProductType GetDeliveredItem()
        {
            return _model.Contract.GetDeliveryItem();
        }

        public void SetTransport(ITransport transport)
        {
            _model.SetTransport(transport);
        }

        public bool IsDelivered()
        {
            if (GetRemainingAmount() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
