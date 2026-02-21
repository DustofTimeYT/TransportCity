public interface IOrder
{
    public IContract GetContract();

    public void SetTransport(ITransport transport);

    public ITransport GetTransport();

    public void SetDeliveredAmount(int amount);

    public int GetDeliveredAmount();

    public int GetRemainingAmount();

    public int GetTotalAmount();

    public ProductType GetDeliveredItem();

    public int GetOrderNumber();

    public bool IsDelivered();

}