using System;
using TMPro;
using UnityEngine;
using Zenject;

public class BankView : MonoBehaviour
{
    [Inject]
    private BankPresenter _presenter;

    [SerializeField] private TextMeshProUGUI _money;

    private void Awake()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        _presenter.UpdateView += OnUpdateView;

        UpdateView();
        Debug.Log($"{this.GetType()} was subscribed");
    }

    private void OnUpdateView()
    {
        UpdateView();
    }

    private void UpdateView()
    {
        _money.text = _presenter.GetMoneyAmount();
    }
}