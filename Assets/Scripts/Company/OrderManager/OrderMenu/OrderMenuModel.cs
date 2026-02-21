using OrderLine;
using System.Collections.Generic;

namespace OrderMenu
{
    public class OrderMenuModel
    {
        public Dictionary<int, OrderLinePresenter> OrderLines { get; private set; }

        public OrderMenuModel()
        {
            OrderLines = new();
        }
    }
}
