using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하기 위해 추가

public class StepManager : MonoBehaviour
{
    // 비행 전 점검 패널
    public GameObject preFlightCheckPanel;

    // 점검 항목 토글들
    public Toggle[] checkToggles;

    // '다음' 버튼
    public Button nextButton;

    // '가이드 보기' 버튼
    public Button showGuideButton;

    // 이 변수는 추후 다른 단계의 패널을 제어하는 데 사용될 수 있습니다.

    void Start()
    {
        // 게임 시작 시, '비행 전 절차' 패널과 '가이드 보기' 버튼을 제어합니다.
        preFlightCheckPanel.SetActive(true);
        nextButton.interactable = false;
        showGuideButton.gameObject.SetActive(false); // '가이드 보기' 버튼은 처음엔 숨겨둡니다.
    }

    void Update()
    {
        CheckAllToggles();
    }

    void CheckAllToggles()
    {
        bool allChecked = true;
        foreach (Toggle toggle in checkToggles)
        {
            if (!toggle.isOn)
            {
                allChecked = false;
                break;
            }
        }
        nextButton.interactable = allChecked;
    }

    // '다음' 버튼에 연결할 함수
    public void NextStep()
    {
        preFlightCheckPanel.SetActive(false);
        showGuideButton.gameObject.SetActive(true); // 패널이 사라진 후 '가이드 보기' 버튼을 보이게 합니다.
        Debug.Log("비행 전 절차 완료! 다음 단계로 이동합니다.");
    }

    // '가이드 보기' 버튼에 연결할 함수
    public void ShowPreFlightPanel()
    {
        preFlightCheckPanel.SetActive(true);
        showGuideButton.gameObject.SetActive(false);
    }
}