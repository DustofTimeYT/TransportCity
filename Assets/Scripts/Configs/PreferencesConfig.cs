using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PreferencesConfig", menuName = "ScriptableObjects/PreferencesConfig", order = 1)]
public class PreferencesConfig : ScriptableObject
{
    /// <summary>
    /// Скорость перемещения камеры
    /// </summary>
    [field: SerializeField]
    [field: Range(0.01f, 1.5f)]
    public float MCSpeed { get; private set; } = 0.5f;

    /// <summary>
    /// Скорость вращения камеры
    /// </summary>
    [field: SerializeField]
    [field: Range(0.5f, 5f)]
    public float RCSpeed { get; private set; } = 3f;

    [SerializeField, SerializedDictionary("Function", "HotKey")]
    private SerializedDictionary<HotKeyFunc, KeyCode> HotKeys;

    public Dictionary<HotKeyFunc, KeyCode> GetHotKeys()
    {
        return HotKeys;
    }

    [field: SerializeField]
    public KeyCode Escape { get; private set; } = KeyCode.Escape;

    [field: SerializeField]
    public KeyCode MCDrag { get; private set; }

    [field: SerializeField]
    public KeyCode MCRight { get; private set; }

    [field: SerializeField]
    public KeyCode MCLeft { get; private set; }

    [field: SerializeField]
    public KeyCode MCFoward { get; private set; }

    [field: SerializeField]
    public KeyCode MCBackward { get; private set; }



    [field: SerializeField]
    public KeyCode RCDrag { get; private set; }

    [field: SerializeField]
    public KeyCode RCRight { get; private set; }

    [field: SerializeField]
    public KeyCode RCLeft { get; private set; }

    [field: SerializeField]
    public KeyCode RCUpward { get; private set; }

    [field: SerializeField]
    public KeyCode RCDownward { get; private set; }

    /// <summary>
    /// Клавиша приближения камеры
    /// </summary>
    [field: SerializeField]
    public KeyCode ZCIn { get; private set; }

    /// <summary>
    /// Клавиша отдаления камеры
    /// </summary>
    [field: SerializeField]
    public KeyCode ZCOut { get; private set; }
}

