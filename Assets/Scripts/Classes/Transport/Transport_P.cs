using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Transport
{
    public class Transport_P : ITransport
    {
        private readonly TransportConfig _transportConfig;
        private Transport_M _trasport_M;
        private Transport_V _trasport_V;

        public Transport_P(TransportConfig tc)
        {
            _transportConfig = tc;
            Load(_transportConfig.transportPref);
            Debug.Log("Transport was creat");
        }

        private void Load(GameObject transportPref)
        {
            _trasport_M = new Transport_M();
            _trasport_V = new Transport_V();
            _trasport_V.Init(transportPref, Vector2Int.zero);
        }

        public void SetRoute(IReadOnlyList<Vector2Int> route)
        {
            _trasport_M.SetRoute(route);
        }
    }
}
