using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;
using DroneController;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    // �ν����Ϳ� ������ ������
    public ThirdPersonController thirdPersonController;
    public AudioSource droneAudioSource;
    public PropellerMovement propellerMovement;

    // CameraSwitcher ��ũ��Ʈ�� ���� �����ϱ� ���� �߰�
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

        // ������ �簳�ϸ� CameraSwitcher�� �ٽ� ������� �������ϴ�.
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

        // ������ �Ͻ������� ��, ��ġ �ʴ� ��� ���� ��Ȱ��ȭ�մϴ�.
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

        // CameraSwitcher�� Update ������ ���߰� �մϴ�.
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