using OrderLine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrderLineView : MonoBehaviour, IView<OrderLinePresenter>, IPointerEnterHandler
{
    private OrderLinePresenter _presenter;

    [SerializeField] private TextMeshProUGUI _orderNumber;
    [SerializeField] private TextMeshProUGUI _deliveredItem;
    [SerializeField] private TextMeshProUGUI _deliveredAmount;
    [SerializeField] private TextMeshProUGUI _totalAmount;

    [SerializeField] private GameObject _submitButton;

    [SerializeField] private ExtendedDropdown _garageDropdown;
    [SerializeField] private ExtendedDropdown _transportDropdown;

    public void Bind(OrderLinePresenter presenter)
    {
        _presenter = presenter;
        Init();

        Subscribe();
        UpdateView();
    }

    private void Init()
    {
        _garageDropdown.Init();
        _transportDropdown.Init();
    }

    private void UpdateView()
    {
        _orderNumber.text = _presenter.GetOrderNumber().ToString();
        _deliveredItem.text = _presenter.GetDeliveredItem();
        _deliveredAmount.text = _presenter.GetDeliveredAmount().ToString();
        _totalAmount.text = _presenter.GetTotalAmount().ToString();
        _garageDropdown.UpdateDropdownOptions(_presenter.GetGarages());
        _transportDropdown.UpdateDropdownOptions(_presenter.GetTransport());

        if (_presenter.IsInProgress())
        {
            _submitButton.SetActive(false);
        }
        else
        {
            _submitButton.SetActive(true);
        }
    }

    private void Subscribe()
    {
        _garageDropdown.SelectOption += OnSelectGarage;
        _transportDropdown.SelectOption += OnSelectTransport;
        _presenter.UpdateView += UpdateView;
    }

    private void OnSelectGarage(string GarageName)
    {
        _presenter.SetGarage(GarageName);
        UpdateView();
    }

    private void OnSelectTransport(string TransportName)
    {
        _presenter.SetTransport(TransportName);
        UpdateView();
    }

    public void Submit()
    {
        _presenter.SubmitOrder();
        UpdateView();
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateView();
    }
}