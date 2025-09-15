using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;
using DroneController; // PropellerMovement�� ����ϱ� ���� �߰�

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    public ThirdPersonController thirdPersonController;
    public AudioSource droneAudioSource;
    public PropellerMovement propellerMovement; // ���� �߰��� ����

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

        // ���� �簳 �� ��ũ��Ʈ�� �����, �����緯�� �ٽ� Ȱ��ȭ�մϴ�.
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

        // ���� �Ͻ����� �� ��ũ��Ʈ�� �����, �����緯�� ��Ȱ��ȭ�մϴ�.
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