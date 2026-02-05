using System;
using System.Collections.Generic;
using UnityEngine;

namespace Transport
{
    public class TransportModel
    {
        private TransportConfig _config;

        public string Name { get; private set; }
        public int MaxSpeed { get; private set; }
        public int MaxCapacity { get; private set; }

        public RoutePresenter Route { get; private set; }

        public TransportModel(TransportConfig config)
        {
            _config = config;
            SetDefault();
        }

        private void SetDefault()
        {
            Name = _config.Name;
            MaxSpeed = _config.MaxSpeed;
            MaxCapacity = _config.MaxCapacity;
        }

        public void SetRoute(RoutePresenter route)
        {
            Route = route;
        }
    }
}