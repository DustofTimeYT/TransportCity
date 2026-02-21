using UnityEngine;

[CreateAssetMenu(fileName = "TransportConfig", menuName = "ScriptableObjects/TransportConfig", order = 1)]
public class TransportConfig : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int MaxSpeed { get; private set; }

    [field: SerializeField]
    public int MaxCapacity { get; private set; }

    [field: SerializeField]
    public int Cost { get; private set; }

    [field: SerializeField]
    public GameObject transportPref { get; private set; }
}
