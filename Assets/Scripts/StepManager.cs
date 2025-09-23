using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro�� ����ϱ� ���� �߰�

public class StepManager : MonoBehaviour
{
    // ���� �� ���� �г�
    public GameObject preFlightCheckPanel;

    // ���� �׸� ��۵�
    public Toggle[] checkToggles;

    // '����' ��ư
    public Button nextButton;

    // '���̵� ����' ��ư
    public Button showGuideButton;

    // �� ������ ���� �ٸ� �ܰ��� �г��� �����ϴ� �� ���� �� �ֽ��ϴ�.

    void Start()
    {
        // ���� ���� ��, '���� �� ����' �гΰ� '���̵� ����' ��ư�� �����մϴ�.
        preFlightCheckPanel.SetActive(true);
        nextButton.interactable = false;
        showGuideButton.gameObject.SetActive(false); // '���̵� ����' ��ư�� ó���� ���ܵӴϴ�.
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

    // '����' ��ư�� ������ �Լ�
    public void NextStep()
    {
        preFlightCheckPanel.SetActive(false);
        showGuideButton.gameObject.SetActive(true); // �г��� ����� �� '���̵� ����' ��ư�� ���̰� �մϴ�.
        Debug.Log("���� �� ���� �Ϸ�! ���� �ܰ�� �̵��մϴ�.");
    }

    // '���̵� ����' ��ư�� ������ �Լ�
    public void ShowPreFlightPanel()
    {
        preFlightCheckPanel.SetActive(true);
        showGuideButton.gameObject.SetActive(false);
    }
}