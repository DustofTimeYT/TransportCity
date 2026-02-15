using System;
using System.Collections.Generic;
using Transport;
using TransportStore;
using UnityEngine;
using UnityEngine.UI;

public class TransportStorePresenter
{
    private TransportStoreModel _model;

    private TransportCatalog _transportCatalog;

    private IGarageManager _garageManager;

    public TransportStorePresenter(TransportCatalog transportCatalog, IGarageManager garageManager)
    {
        _model = new();
        _transportCatalog = transportCatalog;
        _garageManager = garageManager;
        CreateTransportSlots();

        Subscribe();
        Debug.Log($"{this} was created");
    }

    private void Subscribe()
    {
        _garageManager.RefreshGarages += OnRefreshGarages;
    }

    private void OnRefreshGarages()
    {
        RefreshGarages?.Invoke();
    }

    private void CreateTransportSlots()
    {
        foreach (TransportConfig transport in _transportCatalog.Transports)
        {
            TransportSlotPresenter TSPresenter = new TransportSlotPresenter(transport, this);
            _model.TransportSlots.Add(TSPresenter);
        }
    }

    public List<TransportSlotPresenter> GetTransportSlots()
    {
        return  _model.TransportSlots;
    }

    public List<string> GetGarages()
    {
        List<string> garagesName = new List<string>();
        foreach(IGarage garage in _garageManager.GetGarages())
        {
            garagesName.Add(garage.GetName());
        }

        return garagesName;
    }

    public void SetSelectedTransport(TransportConfig transportConfig)
    {
        _model.SetTransportConfig(transportConfig);
        Debug.Log(transportConfig.Name);
    }

    public void CreateTransport()
    {
        TransportPresenter transport = new TransportPresenter(_model.SelectedTransportConfig);
        _garageManager.AddTransport(transport, _model.SelectedGarage);
        Debug.Log($"{transport.GetName()} was created in {_model.SelectedGarage.GetName()}");
    }

    public void SetSelectedGarage(string garageName)
    {
        if ( garageName != null )
        {
            _garageManager.FindGarage(garageName, out IGarage garage);
            _model.SetGarage(garage);
        }
        else
        {
            _model.SetGarage(null);
        }
    }

    public IGarage GetSelectedGarage()
    {
        return _model.SelectedGarage;
    }

    public event Action RefreshGarages;
}
