using System;
public interface IOrderManager
{
    public void AddContract(IContract contract);

    public void FinishRoute(RoutePresenter route);
}