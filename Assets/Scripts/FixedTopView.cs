using UnityEngine;

public class FixedTopView : MonoBehaviour
{
    // ����� ȸ���ص� ī�޶��� ȸ���� �����մϴ�.
    void LateUpdate()
    {
        // ī�޶��� ȸ������ �׻� (90, 0, 0)���� �����մϴ�.
        // Quaternion.Euler(90, 0, 0)�� x������ 90�� ȸ���� ���¸� �ǹ��մϴ�.
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}