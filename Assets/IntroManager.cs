using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class introManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject IntroPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayTime(2));
    }

    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);

        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene(1);
    }


}
