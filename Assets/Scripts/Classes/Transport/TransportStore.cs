using System;
using Transport;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TransportStore : MonoBehaviour
{
    [SerializeField]
    private TransportCatalog _TCatalog;

    [SerializeField]
    private GameObject CardStorePref;

    [SerializeField]
    private Transform CardStoreContainer;

    private Company_P _company_P;

    private void Awake()
    {
        _company_P = new Company_P();
        foreach (TransportConfig transport in _TCatalog.transports)
        {
            CardStore_V cardStore_V = Instantiate(CardStorePref, CardStoreContainer).GetComponent<CardStore_V>();
            CardStore_P cardStore_P = new CardStore_P(transport, cardStore_V, this);
        }
    }

    public void CreateTransport(TransportConfig tc)
    {
        Debug.Log(_company_P.GetStorages().Count);
        _company_P.GetStorages()[0].AddTransport(new Transport_P(tc));
    }

    public void CreateTransport()
    {
        CreateTransport(_TCatalog.transports[0]);
    }
}
