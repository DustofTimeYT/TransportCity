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

    public void OnClick()
    {
        _transportStore.SetSelectedTransport(_model.GetTransportConfig());
    }
}
