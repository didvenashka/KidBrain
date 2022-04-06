using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scenes.Main);
    }

    public void LoadDailyTask()
    {

    }

    public void LoadMemorySquareGame()
    {
        SceneManager.LoadScene(Scenes.MemorySquareGame);
    }

    public void LoadReactTapGame()
    {
        SceneManager.LoadScene(Scenes.ReactTapGame);
    }

    public void LoadMathGame()
    {
        SceneManager.LoadScene(Scenes.MathGame);
    }

    public void LoadQuickEyeGame()
    {
        SceneManager.LoadScene(Scenes.QuickEyeGame);
    }

    public void LoadStats()
    {
        SceneManager.LoadScene(Scenes.Stats);
    }
}

