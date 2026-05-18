using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransportSlotView : MonoBehaviour, IView<TransportSlotPresenter>, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    private TransportSlotPresenter _presenter;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _maxSpeed;
    [SerializeField] private TextMeshProUGUI _maxCapacity;
    [SerializeField] private TextMeshProUGUI _cost;


    public void Bind(TransportSlotPresenter presenter)
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

    public void OnClick()
    {
        _presenter.OnClick();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        _presenter.OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = new Color(255,255,255,0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = new Color(255, 255, 255, 0f);
    }

    private void UpdateView()
    {
        _image.color = new Color(255, 255, 255, 0f);
        _title.text = _presenter.GetName();
        _cost.text = _presenter.GetCost();
        _maxSpeed.text = _presenter.GetMaxSpeed();
        _maxCapacity.text = _presenter.GetMaxCapacity();
    }
}
