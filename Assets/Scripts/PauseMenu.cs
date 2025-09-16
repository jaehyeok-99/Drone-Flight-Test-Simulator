using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;
using DroneController;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    // 인스펙터에 연결할 변수들
    public ThirdPersonController thirdPersonController;
    public AudioSource droneAudioSource;
    public PropellerMovement propellerMovement;

    // CameraSwitcher 스크립트도 직접 제어하기 위해 추가
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

        // 게임을 재개하면 CameraSwitcher가 다시 제어권을 가져갑니다.
        if (cameraSwitcher != null)
        {
            cameraSwitcher.EnableAll();
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // 게임을 일시정지할 때, 원치 않는 모든 것을 비활성화합니다.
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = false;
        }
        if (droneAudioSource != null)
        {
            droneAudioSource.Stop();
        }
        if (propellerMovement != null)
        {
            propellerMovement.enabled = false;
        }

        // CameraSwitcher의 Update 로직도 멈추게 합니다.
        if (cameraSwitcher != null)
        {
            cameraSwitcher.DisableAll();
        }
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