public interface IOrder
{
    public IContract GetContract();

    public ITransport GetTransport();

    public void SetDeliveredAmount(int amount);

    public int GetDeliveredAmount();

    public int GetRemainingAmount();

    public int GetTotalAmount();
}