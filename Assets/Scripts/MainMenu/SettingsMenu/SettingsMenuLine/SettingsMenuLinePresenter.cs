using SettingsMenu;
using System;
using UnityEngine;

namespace SettingsMenuLine
{
    public class SettingsMenuLinePresenter
    {
        SettingsMenuLineModel _model;
        SettingsMenuPresenter _menuPresenter;

        public SettingsMenuLinePresenter(SettingsMenuPresenter menuPresenter, HotKeyFunc functionName, KeyCode key)
        {
            _model = new(functionName, key);
            _menuPresenter = menuPresenter;
        }

        public string GetFunctionName()
        {
            return _model.FunctionName.ToString();
        }

        public string GetKeyName()
        {
            return _model.Key.ToString();
        }

        public void SetNewKey(KeyCode key)
        {
            if(_menuPresenter.SetKey(_model.FunctionName, key))
            {
                _model.SetKey(key);
            }
            UpdateView?.Invoke();
        }

        public void ActivateBlocker() => _menuPresenter.SetActiveBlocker(true);

        public void DeactivateBlocker() => _menuPresenter.SetActiveBlocker(false);

        public event Action UpdateView;
    }
}
