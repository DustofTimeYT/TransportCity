using System;
using System.Collections.Generic;
using System.Linq;
using Transport;
using TransportStore;
using UnityEngine;
using UnityEngine.UI;

public class TransportStorePresenter : IMenuPresenter<TransportSlotPresenter>
{
    private TransportStoreModel _model;

    private TransportCatalog _transportCatalog;

    private IBank _bank;

    private IGarageManager _garageManager;

    private IPathFinder _pathFinder;

    private Transform _transportsContainer;

    public TransportStorePresenter(TransportCatalog transportCatalog, IBank bank,  IGarageManager garageManager, IPathFinder pathFinder)
    {
        _model = new();
        _transportCatalog = transportCatalog;
        _bank = bank;
        _garageManager = garageManager;
        _pathFinder = pathFinder;
        _transportsContainer = new GameObject("Transports").GetComponent<Transform>();
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
        UpdateView?.Invoke();
    }

    private void CreateTransportSlots()
    {
        foreach (TransportConfig transport in _transportCatalog.Transports)
        {
            TransportSlotPresenter TSPresenter = new TransportSlotPresenter(transport, this);
            _model.TransportSlots.Add(TSPresenter);
        }
    }

    public List<TransportSlotPresenter> GetLines()
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
        if (_model.SelectedGarage == null || _model.SelectedTransportConfig == null)
        {
            Debug.Log($"Transport was not created in Garage. Please select Transport or Garage");
            return;
        }

        if (_bank.DebitMoney(_model.SelectedTransportConfig.Cost))
        {
            TransportPresenter transport = new TransportPresenter(_model.SelectedTransportConfig, _pathFinder, _model.SelectedGarage);
            TransportView transportView = GameObject.Instantiate(_model.SelectedTransportConfig.transportPref, _transportsContainer).GetComponent<TransportView>();
            transportView.Bind(transport);
            _garageManager.AddTransport(transport, _model.SelectedGarage);
            Debug.Log($"{transport.GetName()} was created in {_model.SelectedGarage.GetName()}");
        }
        else
        {
            Debug.Log($"Not enough money to buy {_model.SelectedTransportConfig.Name}");
        }
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

    public event Action UpdateView;
}
