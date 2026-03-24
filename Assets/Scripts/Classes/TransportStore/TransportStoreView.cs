using AbsMenu;
using UnityEngine;

public class TransportStoreView : AbsMenuView<TransportStorePresenter, TransportSlotPresenter, TransportSlotView>
{
    [SerializeField]
    private ExtendedDropdown _garageDropdown;

    protected override void Init()
    {
        base.Init();
        _garageDropdown.Init();
    }

    protected override void OnUpdateView()
    {
        base.OnUpdateView();
        RefreshGarages();
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        _garageDropdown.SelectOption += OnSelectGarage;   
    }

    private void OnSelectGarage(string garage)
    {
        _presenter.SetSelectedGarage(garage);
    }

    private void RefreshGarages()
    {
        _garageDropdown.UpdateDropdownOptions(_presenter.GetGarages());
    }


    public void BuyTransport()
    {
        _presenter.CreateTransport();
    }
}
