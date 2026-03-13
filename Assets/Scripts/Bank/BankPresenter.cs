using Bank;
using System;
using UnityEngine;

namespace Bank
{
    public class BankPresenter : IBank
    {
        private BankModel _model;

        public BankPresenter(BankConfig config)
        {
            _model = new BankModel(config);

            Debug.Log($"{this} was created");
        }

        public string GetMoneyAmount()
        {
            return $"{_model.MoneyAmount} rub";
        }

        public bool DebitMoney(int amount)
        {
            if (amount > 0)
            {
                if (amount <= _model.MoneyAmount)
                {
                    _model.SetMoneyAmount(_model.MoneyAmount - amount);
                    UpdateView?.Invoke();
                    return true;
                }
            }

            return false;
        }

        public bool DepositMoney(int amount)
        {
            if (amount > 0)
            {
                _model.SetMoneyAmount(_model.MoneyAmount + amount);
                UpdateView?.Invoke();
                return true;
            }

            return false;
        }

        public event Action UpdateView;
    }
}