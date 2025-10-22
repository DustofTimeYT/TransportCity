using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ITransport
{
    public void SetRoute(IReadOnlyList<Vector2Int> route);

}