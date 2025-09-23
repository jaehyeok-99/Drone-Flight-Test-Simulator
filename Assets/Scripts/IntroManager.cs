using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class introManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public GameObject ModeSelectPanel; // ���� �߰��� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    // UIPanelManager�� ����� ����
    public void ShowModeSelectPanel()
    {
        if (StartPanel != null)
        {
            StartPanel.SetActive(false);
        }

        if (ModeSelectPanel != null)
        {
            ModeSelectPanel.SetActive(true);
        }
    }

    // ������ GoGameScene �Լ��� Ȱ���Ͽ� ���� �ε�
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}