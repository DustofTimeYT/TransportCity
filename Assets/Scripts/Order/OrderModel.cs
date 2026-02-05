

using System;

namespace Order
{
    public class OrderModel
    {
        public IProducer Producer { get; private set; }

        public IConsumer Consumer { get; private set; }

        public Object DeliveryItem { get; private set; }
    }
}
