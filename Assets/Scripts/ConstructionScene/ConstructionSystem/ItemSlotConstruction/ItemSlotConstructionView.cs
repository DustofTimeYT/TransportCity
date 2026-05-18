using ItemSlotConstruction;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotConstructionView : MonoBehaviour, IView<ItemSlotConstructionPresenter>, IPointerClickHandler
{
    private ItemSlotConstructionPresenter _presenter;

    [SerializeField] private TextMeshProUGUI _name;
    public void CreateItem()
    {
        _presenter.CreateConstruction();
    }

    public void Bind(ItemSlotConstructionPresenter presenter)
    {
        _presenter = presenter;
        _name.text = presenter.GetName();
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
        _presenter.CreateConstruction();
    }
}
