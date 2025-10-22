using System;
using System.Collections.Generic;
using UnityEngine;

namespace Transport
{
    internal class Transport_M
    {
        public string Name { get; private set; }
        public int MaxSpeed { get; private set; }
        public IReadOnlyList<Vector2Int> Route { get; private set; }

        public void SetRoute(IReadOnlyList<Vector2Int> route)
        {
            Route = route;
        }
        //private int _currentCost;
        //private int _maxCargoCapacity;
        //private int _duration;
        //private List<Cargo> _currentCargo;
    }
}