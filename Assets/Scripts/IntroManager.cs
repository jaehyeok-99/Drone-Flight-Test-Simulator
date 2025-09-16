using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class introManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public GameObject ModeSelectPanel; // 새로 추가된 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    // UIPanelManager의 기능을 통합
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

    // 기존의 GoGameScene 함수를 활용하여 씬을 로드
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}