using System.Collections.Generic;

namespace OrderManager
{
    public class OrderManagerModel
    {
        public List<IContract> Contracts { get; private set; }

        public Dictionary<int, IOrder> Orders { get; private set; }

        public List<RoutePresenter> Routes { get; private set; }

        public OrderManagerModel()
        {
            Contracts = new();
            Orders = new();
            Routes = new();
        }
    }
}
