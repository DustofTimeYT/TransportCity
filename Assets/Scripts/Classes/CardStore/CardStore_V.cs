using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardStore_V : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private CardStore_P _presenter;

    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private GameObject _selectionZone;

    public void Bind(CardStore_P presenter)
    {
        _presenter = presenter;
        UpdateVisual();
    }

    public void OnClick()
    {
        _presenter.OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectionZone.GetComponent<Image>().color = new Color(255,255,255,0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selectionZone.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
    }

    private void UpdateVisual()
    {
        _selectionZone.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        _title.text = _presenter.GetName();
    }
}
