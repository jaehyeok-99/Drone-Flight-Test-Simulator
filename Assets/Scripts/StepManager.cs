using UnityEngine;
using UnityEngine.UI;

public class StepManager : MonoBehaviour
{
    public GameObject startTestPanel;
    public GameObject checklistPanel;

    void Start()
    {
        ShowStartTestPanel();
    }

    void ShowStartTestPanel()
    {
        if (startTestPanel != null)
        {
            startTestPanel.SetActive(true);
        }
    }

    public void StartTest()
    {
        if (startTestPanel != null)
        {
            startTestPanel.SetActive(false);
        }
        if (checklistPanel != null)
        {
            checklistPanel.SetActive(true);
        }
        Debug.Log("실습 시작! 체크리스트로 이동!");
    }

    public void NextStep()
    {
        if (checklistPanel != null)
        {
            checklistPanel.SetActive(false);
        }
        Debug.Log("체크리스트 완료. 다음 단계로 이동!");
    }
}