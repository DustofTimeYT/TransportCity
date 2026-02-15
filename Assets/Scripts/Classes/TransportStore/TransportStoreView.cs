using System.Collections.Generic;
using System.Linq;
using TMPro;
using Transport;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

public class TransportStoreView : MonoBehaviour
{
    [Inject]
    private TransportStorePresenter _presenter;

    private List<TransportSlotView> _tSViews;

    private int _lastSlotIndex;

    [SerializeField] private int _slotAmount;

    [SerializeField]
    private GameObject TransportSlotPref;

    [SerializeField]
    private Transform TransportSlotContainer;

    [SerializeField]
    private Dropdown GarageDropdown;

    private void Start()
    {
        _tSViews = new List<TransportSlotView>();
        _lastSlotIndex = 0;
        UpdateTransportSlots();
        UpdateGarageDropdown();

        Subscribe();
    }

    private void Subscribe()
    {
        _presenter.RefreshGarages += OnRefreshGarages;
    }

    private void OnRefreshGarages()
    {
        UpdateGarageDropdown();
    }

    private void UpdateGarageDropdown()
    {
        GarageDropdown.ClearOptions();
        GarageDropdown.AddOptions(_presenter.GetGarages());
        GarageDropdown.RefreshShownValue();

        SetSelectedGarage();
    }

    private void SetSelectedGarage()
    {
        IGarage selectedGarage = _presenter.GetSelectedGarage();
        if (selectedGarage != null) 
        {
            string selectedGarageName = selectedGarage.GetName();
        
            List<Dropdown.OptionData> options = GarageDropdown.options.ToList();
            foreach (Dropdown.OptionData option in options)
            {
                if (option.text == selectedGarageName)
                {
                    GarageDropdown.SetValueWithoutNotify(options.IndexOf(option));
                    return;
                }
            }
        }

        if (GarageDropdown.options.Count == 0)
        {
            _presenter.SetSelectedGarage(null);
        }
        else
        {
            _presenter.SetSelectedGarage(GarageDropdown.options[0].text);
        }
    }

    public void NextPage()
    {
        if (_lastSlotIndex < _presenter.GetTransportSlots().Count - _slotAmount)
        {
            _lastSlotIndex = _lastSlotIndex + _slotAmount;
        }
        UpdateTransportSlots();
    }
    public void PreviousPage()
    {
        _lastSlotIndex = _lastSlotIndex - _slotAmount;
        if (_lastSlotIndex < 0)
        {
            _lastSlotIndex = 0;
        }
        UpdateTransportSlots();
    }

    private void UpdateTransportSlots()
    {
        int index = 0;
        foreach (TransportSlotPresenter transportSlot in _presenter.GetTransportSlots().Skip(_lastSlotIndex).Take(_slotAmount))
        {
            UpdateTransportSlot(transportSlot, index);
            index++;
        }
    }

    private void UpdateTransportSlot(TransportSlotPresenter transportSlot, int index)
    {
        if(_tSViews.Count < _slotAmount)
        {
            TransportSlotView TSView = Instantiate(TransportSlotPref, TransportSlotContainer).GetComponent<TransportSlotView>();
            if (TSView == null)
            {
                Debug.LogError("TransportSlotView cannot be NULL");
                return;
            }
            _tSViews.Add(TSView);
        }
        _tSViews[index].Bind(transportSlot);

    }

    public void ChangeSelectedGarage()
    {
        _presenter.SetSelectedGarage(GarageDropdown.captionText.text);
    }

    public void BuyTransport()
    {
        _presenter.CreateTransport();
    }
}
