using Contract;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContractView : MonoBehaviour, IPointerClickHandler
{
    private ContractPresenter _presenter;
    private ContractBoardView _contractBoardView;

    [SerializeField] private TextMeshProUGUI ContractName;
    [SerializeField] private TextMeshProUGUI ContractAmount;
    [SerializeField] private TextMeshProUGUI ProducerName;
    [SerializeField] private TextMeshProUGUI ConsumerName;

    public void Bind(ContractPresenter presenter, ContractBoardView contractBoardView)
    {
        _presenter = presenter;
        _contractBoardView = contractBoardView;
        UpdateView();
    }

    private void UpdateView()
    {
        ContractName.text = _presenter.GetDeliveryItem().ToString();
        ContractAmount.text = _presenter.GetAmountItems().ToString();
        ProducerName.text = _presenter.GetProducerName();
        ConsumerName.text = _presenter.GetConsumerName();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _contractBoardView.SelectContract(_presenter);
    }
}