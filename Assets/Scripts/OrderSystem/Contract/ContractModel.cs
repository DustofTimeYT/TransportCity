using System;

namespace Contract
{
    public class ContractModel
    {
        public IProducer Producer { get; private set; }

        public IConsumer Consumer { get; private set; }

        public ProductType DeliveryItem { get; private set; }

        public int Amount { get; private set; }

        public int MoneyAmount { get; private set; }

        public ContractModel(IProducer producer, IConsumer consumer, ProductType product, int amount, int moneyAmount)
        {
            Producer = producer;
            Consumer = consumer;
            DeliveryItem = product;
            Amount = amount;
            MoneyAmount = moneyAmount;
        }
    }
}
