namespace Order
{
    public class OrderModel
    {
        public IContract Contract { get; private set; }

        public int DeliveredAmount { get; private set; }

        public ITransport Transport { get; private set; } 

        public OrderModel(IContract contract)
        {
            Contract = contract;
            DeliveredAmount = 0;
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

            DeliveredAmount = deliveredAmount;
            return true;
        }
    }
}