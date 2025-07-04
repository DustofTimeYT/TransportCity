using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PreferencesConfig", menuName = "ScriptableObjects/PreferencesConfig", order = 1)]
public class PreferencesConfig : ScriptableObject
{
    /// <summary>
    /// Скорость перемещения камеры
    /// </summary>
    [field: SerializeField]
    [field: Range(0.01f, 1.5f)]
    public float MCSpeed { get; private set; }

    /// <summary>
    /// Скорость вращения камеры
    /// </summary>
    [field: SerializeField]
    [field: Range(0.5f, 5f)]
    public float RCSpeed { get; private set; }

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

