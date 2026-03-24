using SettingsMenuLine;
using System.Collections.Generic;

namespace SettingsMenu
{
    public class SettingsMenuModel
    {
        public Dictionary<HotKeyFunc, SettingsMenuLinePresenter> Lines { get; private set; }

        public SettingsMenuModel()
        {
            Lines = new();
        }
    }
}
