using Order;

public class OrderView
{
    private OrderPresenter _presenter;

    public void Bind(OrderPresenter presenter)
    {
        _presenter = presenter;
    }
}