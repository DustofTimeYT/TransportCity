using ContractSystem;
using ContractBoard;
using System.Collections.Generic;
using Contract;
public class ContractBoardPresenter
{
    private ContractBoardModel _model;
    private ContractGenerator _generator;
    private IOrderManager _orderManager;

    public ContractBoardPresenter(ContractGenerator generator)
    {
        _model = new ContractBoardModel();
        _generator = generator;
       // _orderManager = orderManager;
    }

    public List<IContract> GetContracts()
    {
        return _model.Contracts;
    }

    public ContractPresenter GenerateContract()
    {
        _generator.GenerateContract(out var contract);
        _model.Contracts.Add(contract);
        return contract;
    }

    public void SelectContract(IContract contract)
    {
        _orderManager.AddContract(contract);
    }
}