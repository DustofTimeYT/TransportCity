using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Transport
{
    public class TransportModel
    {
        private TransportConfig _config;

        private static Dictionary<string, int> _transportNames = new Dictionary<string, int>();

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
            SetValidName();
            MaxSpeed = _config.MaxSpeed;
            MaxCapacity = _config.MaxCapacity;
        }

        private void SetValidName()
        {
            if (_transportNames.ContainsKey(_config.Name))
            {
                if (_transportNames.TryGetValue(_config.Name, out int number))
                {
                    Name = $"{_config.Name} {number}";
                    _transportNames.Remove(_config.Name);
                    _transportNames.Add(_config.Name, number + 1);

                }
                else
                {
                    Debug.LogError($"Couldn't assign a name to {_config.Name}");
                    return;
                }
            }
            else
            {
                Name = _config.Name;
                _transportNames.Add(_config.Name, 1);
            }
        }


        public void SetRoute(RoutePresenter route)
        {
            Route = route;
        }
    }
}