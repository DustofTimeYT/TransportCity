using System;
using UnityEngine;

namespace SettingsMenuLine
{
    public class SettingsMenuLineModel
    {
        public HotKeyFunc FunctionName { get; private set; }

        public KeyCode Key { get; private set; }

        public SettingsMenuLineModel(HotKeyFunc functionName, KeyCode key)
        {
            FunctionName = functionName;
            Key = key;
        }

        public void SetKey(KeyCode key)
        {
            Key = key;
        }
    }
}
