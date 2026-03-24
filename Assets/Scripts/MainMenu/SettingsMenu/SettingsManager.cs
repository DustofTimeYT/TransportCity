using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// Система,отвечающая за действия пользователя в меню настроек
/// </summary>

public class SettingsManager
{
    private PreferencesConfig _defaultConfig;
    private string SavePath => Application.persistentDataPath + "/hotkeys.json";

    private Dictionary<HotKeyFunc, KeyCode> HotKeys;

    public SettingsManager(PreferencesConfig defaultConfig)
    {
        _defaultConfig = defaultConfig;
        Load();
    }

    private void Load()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            HotKeys = JsonConvert.DeserializeObject<Dictionary<HotKeyFunc, KeyCode>>(json);
        }
        else
        {
            HotKeys = _defaultConfig.GetHotKeys();
            Save();
        }
    }
    public void Save()
    {
        string json = JsonConvert.SerializeObject(HotKeys, Formatting.Indented);
        File.WriteAllText(SavePath, json);
    }

    public bool SetKey(HotKeyFunc functionName, KeyCode key)
    {
        if (!HotKeys.ContainsKey(functionName)) return false;

        if (key == HotKeys[HotKeyFunc.Escape]) return false;

        HotKeys[functionName] = key;

        return true;
    }

    public Dictionary<HotKeyFunc, KeyCode> GetHotKeys()
    {
        return HotKeys;
    }

    public KeyCode GetHotKey(HotKeyFunc function)
    {
        return HotKeys[function];
    }

}
