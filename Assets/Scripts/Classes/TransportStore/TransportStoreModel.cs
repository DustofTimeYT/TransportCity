using System.Collections.Generic;
using System.Net.Sockets;

namespace TransportStore
{
    public class TransportStoreModel
    {
        public List<TransportSlotPresenter> TransportSlots { get; private set; }
        public IGarage SelectedGarage { get; private set; }

        public TransportConfig SelectedTransportConfig { get; private set; }

        public TransportStoreModel()
        {
            TransportSlots = new List<TransportSlotPresenter>();
        }

        public void SetGarage(IGarage garage)
        {
            SelectedGarage = garage;
        }

        public void SetTransportConfig(TransportConfig transportConfig)
        {
            SelectedTransportConfig = transportConfig;
        }
    }
}
