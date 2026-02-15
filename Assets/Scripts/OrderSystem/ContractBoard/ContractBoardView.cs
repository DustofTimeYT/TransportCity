using Contract;
using ContractBoard;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ContractBoardView : MonoBehaviour
{
    [Inject]
    private ContractBoardPresenter _presenter;

    [SerializeField]
    private GameObject ContractPrefab;

    [SerializeField]
    private Transform ContractBoard;

    public void Refresh()
    {
        GenerateContract();
    }

    private void GenerateContract()
    {
        ContractPresenter contract = _presenter.GenerateContract();
        ContractView contractView= Instantiate(ContractPrefab,ContractBoard).GetComponent<ContractView>();
        contractView.Bind(contract, this);
    }

    public void SelectContract(IContract contract)
    {
        _presenter.SelectContract(contract);
    }
}
