using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Система,отвечающая за действия пользователя в меню настроек
/// </summary>

public class SettingsMenuManagerScr : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    GameObject settingsMenuUI;

    public void Cancel()
    {
        if (settingsMenuUI == null)
        {
            Debug.LogError("Settings menu manager Settings Menu UI field is Null!");
            return;
        }

        if (mainMenuUI == null)
        {
            Debug.LogError("Settings menu manager Main Menu UI field is Null!");
            return;
        }

        settingsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
