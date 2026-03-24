using SettingsMenuLine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SettingsMenu
{
    public class SettingsMenuPresenter : IMenuPresenter<SettingsMenuLinePresenter>
    {
        private SettingsMenuModel _model;
        private SettingsManager _settingsManager;

        public SettingsMenuPresenter(SettingsManager settingsManager)
        {
            _model = new();
            _settingsManager = settingsManager;
            CreateLines();
        }

        private void CreateLines()
        {
            foreach (var line in _settingsManager.GetHotKeys())
            {
                _model.Lines.Add(line.Key, new (this ,line.Key, line.Value));
            }
        }

        public void SetActiveBlocker(bool active)
        {
            SetActive?.Invoke(active);
        }

        public bool SetKey(HotKeyFunc functionName, KeyCode key)
        {
            return _settingsManager.SetKey(functionName, key);
        }

        public List<SettingsMenuLinePresenter> GetLines()
        {
            return _model.Lines.Values.ToList();
        }
        
        public void Save() => _settingsManager.Save();

        public event Action UpdateView;
        public event Action<bool> SetActive;
    }
}
