using UnityEngine;

public class FixedTopView : MonoBehaviour
{
    // 드론이 회전해도 카메라의 회전은 고정합니다.
    void LateUpdate()
    {
        // 카메라의 회전값을 항상 (90, 0, 0)으로 고정합니다.
        // Quaternion.Euler(90, 0, 0)은 x축으로 90도 회전한 상태를 의미합니다.
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}