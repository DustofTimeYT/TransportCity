using UnityEngine.SceneManagement;

/// <summary>
/// �������� ������������ ����� ������� ����
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