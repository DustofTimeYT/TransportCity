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

        public Vector2Int Position { get; private set; }
        public TransportStatus Status { get; private set; }
        public IGarage Garage { get; private set; }
        public string Name { get; private set; }
        public int MaxSpeed { get; private set; }
        public int MaxCapacity { get; private set; }

        public List<ICargoItem> Cargos { get; private set; }

        public RoutePresenter Route { get; private set; }

        public TransportModel(TransportConfig config, IGarage garage)
        {
            _config = config;
            SetGarage(garage);
            SetDefault();
        }

        private void SetDefault()
        {
            SetValidName();
            MaxSpeed = _config.MaxSpeed;
            MaxCapacity = _config.MaxCapacity;
            SetPosition(Garage.GetTilePosition());
            SetStatus(TransportStatus.Idle);
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

        public void SetStatus(TransportStatus status)
        {
            Status = status;
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
        }

        public void SetGarage(IGarage garage)
        {
            Garage = garage;
        }

        public void SetRoute(RoutePresenter route)
        {
            Route = route;
        }

        public void SetCargo(List<ICargoItem> cargos)
        {
            if (cargos.Count <= MaxCapacity)
            {
                Cargos = cargos;
            }
        }
    }
}