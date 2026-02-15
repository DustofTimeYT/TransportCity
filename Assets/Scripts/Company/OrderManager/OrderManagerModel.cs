using Order;
using System.Collections.Generic;

namespace OrderManager
{
    public class OrderManagerModel
    {
        public List<IContract> Contracts {  get; private set; }

        public List<OrderPresenter> Orders { get; private set; }

        public OrderManagerModel()
        {
            Contracts = new List<IContract>();
            Orders = new List<OrderPresenter>();
        }
    }
}
