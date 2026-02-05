using Transport;
using UnityEngine;

namespace Transport
{
    public class TransportPresenter : ITransport
    {
        private readonly TransportConfig _transportConfig;
        private TransportModel _trasportModel;
        private TransportView _trasportView;

        public TransportPresenter(TransportConfig tConfig)
        {
            _transportConfig = tConfig;
            _trasportModel = new TransportModel(tConfig);
            Load(_transportConfig.transportPref);
            Debug.Log("Transport was creat");
        }

        private void Load(GameObject transportPref)
        {
            _trasportView = new TransportView();
            _trasportView.Init(transportPref, Vector2Int.zero);
        }

        public void SetRoute(RoutePresenter route)
        {
            _trasportModel.SetRoute(route);
        }
    }
}
