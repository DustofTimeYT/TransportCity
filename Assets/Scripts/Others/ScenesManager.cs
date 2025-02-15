using UnityEngine.SceneManagement;

/// <summary>
/// Менеджер переключения между сценами игры
/// </summary>

public class ScenesManager
{
    public static void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void OpenGame()
    {
        SceneManager.LoadScene(1);
    }
}