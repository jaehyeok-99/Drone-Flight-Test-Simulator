using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    // CameraSwitcher 스크립트 참조
    public CameraSwitcher cameraSwitcher;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // 게임을 재개할 때, CameraSwitcher에게 상태를 복원하도록 요청합니다.
        if (cameraSwitcher != null)
        {
            cameraSwitcher.RestoreState();
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadHomeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}