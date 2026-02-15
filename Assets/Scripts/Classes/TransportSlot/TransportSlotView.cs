using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransportSlotView : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    private TransportSlotPresenter _presenter;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _title;


    public void Bind(TransportSlotPresenter presenter)
    {
        _presenter = presenter;
        UpdateView();
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
    }
}
