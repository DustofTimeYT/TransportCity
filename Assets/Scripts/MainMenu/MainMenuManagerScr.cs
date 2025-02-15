using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������� ��������� �������� ������������ � ������� ����
/// </summary>

public class MainMenuManagerScr : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject settingsMenuUI;

    [SerializeField]
    private Button continueBtn;

    private void Awake()
    {
        SetByDefaultMainMenu();
    }

    private void Update()
    {
        ChangeStatusContinueBtn();
    }

    /// <summary>
    /// ��������� �� ��������� �������� �������� ����
    /// </summary>
    
    private void SetByDefaultMainMenu()
    {
        if (settingsMenuUI == null)
        {
            Debug.LogError("Main menu manager Settings Menu UI field is Null!");
            return;
        }

        if (mainMenuUI == null)
        {
            Debug.LogError("Main menu manager Main Menu UI field is Null!");
            return;
        }
       
            mainMenuUI.SetActive(true);
            settingsMenuUI.SetActive(false);
    }

    /// <summary>
    /// ������� ����������� ����� ������� ����
    /// </summary>

    public void Continue()
    {
        Debug.Log("Game run");
    }

    /// <summary>
    /// ������� �������� ����� ����
    /// </summary>

    public void NewGame()
    {
        ScenesManager.OpenGame();
        Debug.Log("New game run");
    }

    /// <summary>
    /// ������� �������� �������� �������� 
    /// </summary>

    public void OpenSettings()
    {
        if (settingsMenuUI == null)
        {
            Debug.LogError("Main menu manager Settings Menu UI field is Null!");
            return;
        }

        if (mainMenuUI == null)
        {
            Debug.LogError("Main menu manager Main Menu UI field is Null!");
            return;
        }

        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        Debug.Log("Game settings are open");

    }

    /// <summary>
    /// ������� ������ �� ����
    /// </summary>

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game stop");
    }


    /// <summary>
    /// ����� �������� ������������� ����������
    /// </summary>

    private void ChangeStatusContinueBtn()
    {
        if (continueBtn.interactable && !File.Exists($"{Application.persistentDataPath}/MySaveData.dat"))
        {
            ChangeStatusContinueBtn(false);
        }
        else if (!continueBtn.interactable && File.Exists($"{Application.persistentDataPath}/MySaveData.dat"))
        {
            ChangeStatusContinueBtn(true);
        }
    }

    /// <summary>
    /// ����� ���������� ����������� �������������� � ������� Continue
    /// </summary>
    /// <param name="interactable">���������� ����������� ��������������</param>

    private void ChangeStatusContinueBtn(bool interactable)
    {
        if (continueBtn != null)
        {
            continueBtn.interactable = interactable;
        }
        else
        {
            Debug.LogError($"Main menu manager Continue Button field is Null!");
        }
    }
}
