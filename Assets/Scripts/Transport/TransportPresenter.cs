using Transport;
using UnityEngine;

namespace Transport
{
    public class TransportPresenter : ITransport
    {
        private TransportModel _model;

        public TransportPresenter(TransportConfig tConfig)
        {
            _model = new TransportModel(tConfig);
        }

        public string GetName()
        {
            return _model.Name;
        }

        public void SetRoute(RoutePresenter route)
        {
            _model.SetRoute(route);
        }
    }
}
