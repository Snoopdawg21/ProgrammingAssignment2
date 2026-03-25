using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void BeginGame()
    {
        SceneManager.LoadScene("GameScene");

        Time.timeScale = 1;
    }
}
