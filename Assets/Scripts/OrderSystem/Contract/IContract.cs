public interface IContract
{
    public IProducer GetProducer();

    public IConsumer GetConsumer();

    public ProductType GetDeliveryItem();

    public int GetDeliveryAmount();
}
