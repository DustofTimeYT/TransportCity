using ContractLine;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContractLineView : MonoBehaviour, IView<ContractLinePresenter>, IPointerClickHandler
{
    private ContractLinePresenter _presenter;

    [SerializeField] private TextMeshProUGUI ContractName;
    [SerializeField] private TextMeshProUGUI ContractAmount;
    [SerializeField] private TextMeshProUGUI ProducerName;
    [SerializeField] private TextMeshProUGUI ConsumerName;
    [SerializeField] private TextMeshProUGUI Money;

    private void UpdateView()
    {
        ContractName.text = _presenter.GetDeliveryItem().ToString();
        ContractAmount.text = _presenter.GetDeliveryAmount().ToString();
        ProducerName.text = _presenter.GetProducerName();
        ConsumerName.text = _presenter.GetConsumerName();
        Money.text = _presenter.GetMoneyAmount();
    }

    public void Bind(ContractLinePresenter presenter)
    {
        _presenter = presenter;

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


    public void OnPointerClick(PointerEventData eventData)
    {
        _presenter.SelectContract();
        Hide();
    }
}