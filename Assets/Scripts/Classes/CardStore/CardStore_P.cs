using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardStore_P
{
    private readonly TransportStore _transportStore;
    private readonly CardStore_M _model;
    private readonly CardStore_V _view;

    public CardStore_P(TransportConfig tc, CardStore_V view, TransportStore transportStore)
    {
        _model = new CardStore_M(tc);
        _view = view;
        _transportStore = transportStore;
        _view.Bind(this);
    }

    public string GetName()
    {
        return _model.GetName();
    }

    public void OnClick()
    {
        _transportStore.CreateTransport(_model.GetTransportConfig());
    }
}
