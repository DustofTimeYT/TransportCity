using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        ScenesManager.OpenMainMenu();
        Debug.Log("Back to Main menu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game stop");
    }
}