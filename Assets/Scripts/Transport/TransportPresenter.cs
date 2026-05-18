using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Transport;
using UnityEngine;

namespace Transport
{
    public class TransportPresenter : ITransport
    {
        private TransportModel _model;
        private IPathFinder _pathFinder;

        public TransportPresenter(TransportConfig config, IPathFinder pathFinder, IGarage garage)
        {
            _model = new TransportModel(config, garage);
            _pathFinder = pathFinder;
        }

        public Vector2Int GetPosition()
        {
            return _model.Position;
        }

        public void BegineRoute()
        {
            _model.Route.RouteStarted();
            StartRoute?.Invoke();

        }

        private IReadOnlyList<Vector2Int> FindPath(Vector2Int endPoint)
        {
            _pathFinder.TryPathFind(_model.Position, endPoint, out IReadOnlyList<Vector2Int> path);
            return path;
        }

        public IReadOnlyList<Vector2Int> FindPathToLoad()
        {
            return FindPath(GetLoadPosition());
        }

        public IReadOnlyList<Vector2Int> FindPathToUnload()
        {
            return FindPath(GetUnloadPosition());
        }

        public IReadOnlyList<Vector2Int> FindPathToBase()
        {
            return FindPath(_model.Garage.GetTilePosition());
        }

        public Vector2Int GetLoadPosition()
        {
            return _model.Route.GetLoadPosition();
        }

        public Vector2Int GetUnloadPosition()
        {
            return _model.Route.GetUnloadPosition();
        }

        public TransportStatus GetStatus()
        {
            return _model.Status;
        }

        public void ReturnToBase()
        {
             StartReturnToBase?.Invoke();
        }

        public void SetPosition(Vector3 position)
        {
            _model.SetPosition(new Vector2Int((int)position.x, (int)position.z));
        }

        public string GetName()
        {
            return _model.Name;
        }

        public void SetRoute(RoutePresenter route)
        {
            _model.SetRoute(route);
        }

        public int GetMaxCapacity()
        {
            return _model.MaxCapacity;
        }

        public int GetMaxSpeed()
        {
            return _model.MaxSpeed;
        }

        public void LoadCargo()
        {
            var cargo = _model.Route.GetProducer().GetCargo(_model.Route);
            _model.SetCargo(cargo);
        }

        public void UnloadCargo()
        {
            _model.Route.GetConsumer().HandOverCargo(_model.Cargos, _model.Route);
            _model.Cargos.Clear();
        }

        public event Action StartRoute;

        public event Action StartReturnToBase;
    }
}
