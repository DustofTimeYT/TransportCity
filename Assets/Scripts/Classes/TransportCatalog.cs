using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransportCatalog", menuName = "ScriptableObjects/TransportCatalog", order = 1)]
public class TransportCatalog : ScriptableObject
{
    [field: SerializeField]
    public List<TransportConfig> Transports {  get; private set; }
}
