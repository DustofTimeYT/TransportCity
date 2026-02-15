using Contract;

namespace Contract
{
    public class ContractPresenter : IContract
    {
        private ContractModel _model;

        public ContractPresenter(IProducer producer, IConsumer consumer, ProductType product, int amount)
        {
            _model = new ContractModel(producer, consumer, product, amount);
        }

        public IProducer GetProducer()
        {
            return _model.Producer;
        }

        public IConsumer GetConsumer()
        {
            return _model.Consumer;
        }

        public string GetProducerName()
        {
            return _model.Producer.GetName();
        }
        
        public string GetConsumerName()
        {
            return _model.Consumer.GetName();
        }
        public ProductType GetDeliveryItem()
        {
            return _model.DeliveryItem;
        }
        public int GetDeliveryAmount()
        {
            return _model.Amount;
        }
    }
}