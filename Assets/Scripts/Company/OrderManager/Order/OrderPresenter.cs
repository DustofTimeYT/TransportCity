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
            return _model.Contract.GetDeliveryAmount() - _model.DeliveredAmount;
        }

        public int GetTotalAmount()
        {
            return _model.Contract.GetDeliveryAmount();
        }

        public ITransport GetTransport()
        {
            return _model.Transport;
        }

    }
}
