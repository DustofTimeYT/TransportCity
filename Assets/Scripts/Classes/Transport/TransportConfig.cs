using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "TransportConfig", menuName = "ScriptableObjects/TransportConfig", order = 1)]
public class TransportConfig : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int MaxSpeed { get; private set; }

    [field: SerializeField]
    public GameObject transportPref { get; private set; }
}
