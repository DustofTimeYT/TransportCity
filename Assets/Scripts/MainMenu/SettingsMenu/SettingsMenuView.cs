using AbsMenu;
using SettingsMenu;
using SettingsMenuLine;
using System;
using UnityEngine;

public class SettingsMenuView : AbsMenuView<SettingsMenuPresenter, SettingsMenuLinePresenter, SettingsMenuLineView>
{
    [SerializeField]
    private GameObject mainMenuUI;

    [SerializeField]
    private GameObject _blocker;
    private void SetActive(bool active)
    {
        _blocker.SetActive(active);
    }

    private void OnDisable()
    {
        _presenter.Save();
    }

    protected override void Init()
    {
        base.Init();

        _blocker.SetActive(false);
    }

    protected override void Subscribe()
    {
        base.Subscribe();

        _presenter.SetActive += SetActive;
    }


    public void Cancel()
    {
        if (mainMenuUI == null)
        {
            Debug.LogError("Settings menu manager Main Menu UI field is Null!");
            return;
        }

        gameObject.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}