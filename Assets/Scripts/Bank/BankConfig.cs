using UnityEngine;

[CreateAssetMenu(fileName = "BankConfig", menuName = "ScriptableObjects/BankConfig", order = 1)]

public class BankConfig : ScriptableObject
{
    [field: SerializeField]
    public int MoneyAmount { get; private set; }
}