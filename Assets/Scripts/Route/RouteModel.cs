using UnityEngine;

namespace Route
{
    public class RouteModel
    {
        public IOrder Order { get; private set; }

        public ProductType DeliveryItem { get; private set; }

        public int CargoAmount { get; private set; }

        public Vector2Int LoadPosition { get; private set; }

        public Vector2Int UnloadPosition { get; private set; }
        
        public RouteStatus Status { get; private set; }

        public RouteModel(IOrder order, int cargoAmount)
        {
            Order = order;
            LoadPosition = Order.GetContract().GetProducer().GetTilePosition();
            UnloadPosition = Order.GetContract().GetConsumer().GetTilePosition();
            DeliveryItem = Order.GetDeliveredItem();
            CargoAmount = cargoAmount;
            SetStatus(RouteStatus.InQueuing);
        }

        public void SetStatus(RouteStatus status)
        {
            Status = status;
        }
    }
}
