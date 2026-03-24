using SettingsMenuLine;
using System;
using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenuLineView : MonoBehaviour, IPointerClickHandler, IView<SettingsMenuLinePresenter>
{
    private SettingsMenuLinePresenter _presenter;

    [SerializeField] private TextMeshProUGUI FunctionName;
    [SerializeField] private TextMeshProUGUI KeyName;

    [SerializeField] private string _placeholder = "...";

    public void Bind(SettingsMenuLinePresenter presenter)
    {
        _presenter = presenter;

        Subscribe();
        UpdateView();
    }

    private void Subscribe()
    {
        _presenter.UpdateView += UpdateView;
    }

    private void UpdateView()
    {
        FunctionName.text = _presenter.GetFunctionName();
        KeyName.text = _presenter.GetKeyName();
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
        if (_presenter.GetFunctionName() != HotKeyFunc.Escape.ToString())
        {
            StartCoroutine(ChangeKey());
        }
    }

    private IEnumerator ChangeKey()
    {
        KeyName.text = _placeholder;
        _presenter.ActivateBlocker();

        yield return new WaitWhile(() => !Input.anyKeyDown);

        _presenter.SetNewKey(GetAnyKey());
        _presenter.DeactivateBlocker();
    }

    private KeyCode GetAnyKey()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key; 
            }
        }

        return KeyCode.None;
    }
}