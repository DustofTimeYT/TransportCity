using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TransportSlotPresenter
{
    private readonly TransportStorePresenter _transportStore;
    private readonly TransportSlotModel _model;

    public TransportSlotPresenter(TransportConfig tc, TransportStorePresenter transportStore)
    {
        _model = new TransportSlotModel(tc);
        _transportStore = transportStore;
    }

    public string GetName()
    {
        return _model.GetName();
    }

    public string GetMaxSpeed()
    {
        return $"Max speed: {_model.GetMaxSpeed()} km/h";
    }

    public string GetMaxCapacity()
    {
        return $"Max cargo capacity: {_model.GetMaxCapacity()}";
    }

    public string GetCost()
    {
        return $"{_model.GetCost()} rub";
    }

    public void OnClick()
    {
        _transportStore.CreateTransport(_model.GetTransportConfig());
    }
}
