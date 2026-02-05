using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TransportView : MonoBehaviour
{
    private GameObject _prefab;

    public void Init(GameObject transportPref, Vector2Int spawnpoint)
    {
        this._prefab = transportPref;
        Instantiate(_prefab, new Vector3(spawnpoint.x, 0, spawnpoint.y), Quaternion.identity);
    }
}
