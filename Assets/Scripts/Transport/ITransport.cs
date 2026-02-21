
public interface ITransport : IOption
{
    public void SetRoute(RoutePresenter route);

    public int GetMaxCapacity();

    public void BegineRoute();

    public void ReturnToBase();


}