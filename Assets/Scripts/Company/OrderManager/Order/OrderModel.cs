namespace Order
{
    public class OrderModel
    {
        private static int OrderId = 0;

        public IContract Contract { get; private set; }

        public int OrderNumber { get; private set; }

        public int DeliveredAmount { get; private set; }

        public ITransport Transport { get; private set; } 

        public OrderModel(IContract contract)
        {
            Contract = contract;
            DeliveredAmount = 0;
            SetOrderNumber();
        }

        private void SetOrderNumber()
        {
            OrderId += 1;
            OrderNumber = OrderId;
        }

        public bool TrySetDeliveredAmount(int deliveredAmount)
        {
            if (deliveredAmount <= 0)
            {
                return false;
            }

            if (deliveredAmount + DeliveredAmount > Contract.GetDeliveryAmount())
            {
                return false;
            }

            DeliveredAmount += deliveredAmount;
            return true;
        }

        public void SetTransport(ITransport transport)
        {
            Transport = transport;
        }
    }
}