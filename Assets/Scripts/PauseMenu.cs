using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;
using DroneController; // PropellerMovement를 사용하기 위해 추가

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    public ThirdPersonController thirdPersonController;
    public AudioSource droneAudioSource;
    public PropellerMovement propellerMovement; // 새로 추가된 변수

    private string previousSceneName;

    void Awake()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // 게임 재개 시 스크립트와 오디오, 프로펠러를 다시 활성화합니다.
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = true;
        }
        if (droneAudioSource != null)
        {
            droneAudioSource.Play();
        }
        if (propellerMovement != null)
        {
            propellerMovement.enabled = true;
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // 게임 일시정지 시 스크립트와 오디오, 프로펠러를 비활성화합니다.
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
    }

    public void LoadHomeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeSceneName");
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBack()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(previousSceneName);
    }
}