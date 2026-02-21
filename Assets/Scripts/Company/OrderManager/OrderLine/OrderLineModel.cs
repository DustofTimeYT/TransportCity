namespace OrderLine
{
    public class OrderLineModel
    {
        public IOrder Order { get; private set; }

        public IGarage Garage { get; private set; }

        public bool IsInProgress { get; private set; }

        public OrderLineModel(IOrder order)
        {
            Order = order;
            IsInProgress = false;
        }

        public void SetGarage(IGarage garage)
        {
            Garage = garage;
        }

        public void SetOrder(IOrder order)
        {
            Order = order;
        }

        public void SetInProgress()
        {
            IsInProgress = true;
        }
    }
}