using ContractSystem;
using ContractBoard;
using System.Collections.Generic;
using ContractLine;
using System;
using UnityEngine;
public class ContractBoardPresenter : IMenuPresenter<ContractLinePresenter>
{
    private ContractBoardModel _model;
    private ContractGenerator _generator;
    private IOrderManager _orderManager;


    public ContractBoardPresenter(ContractGenerator generator, IOrderManager orderManager)
    {
        _model = new ContractBoardModel();
        _generator = generator;
        _orderManager = orderManager;
    }

    public void GenerateContract()
    {
        if (!_generator.GenerateContract(out var contract))
        {
            Debug.Log("Create Contract was not sucsess. Please select Producer or Consumer");
            return;
        }
        ContractLinePresenter contractLine = new(contract, this);
        _model.Contracts.Add(contractLine);

        UpdateView?.Invoke();
    }

    public void SelectContract(ContractLinePresenter contract)
    {
        _orderManager.AddContract(contract.GetContract());
        _model.Contracts.Remove(contract);
    }

    public List<ContractLinePresenter> GetLines()
    {
        return _model.Contracts;
    }
    
    public event Action UpdateView;
}