using UnityEngine;

public class DroneInteraction : MonoBehaviour
{
    private CameraSwitcher cameraSwitcher;
    private Renderer controlAreaRenderer;
    private Material instantiatedMaterial;

    public Color defaultColor = Color.white;
    public Color activeColor = Color.green;

    void Start()
    {
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        controlAreaRenderer = GetComponent<Renderer>();

        if (controlAreaRenderer != null)
        {
            instantiatedMaterial = controlAreaRenderer.material;
            instantiatedMaterial.color = defaultColor;
        }

        if (cameraSwitcher == null)
        {
            Debug.LogError("씬에서 CameraSwitcher 스크립트를 찾을 수 없습니다.");
        }
    }

    // 트리거 안에 머무는 동안 계속 호출됩니다.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 조종 구역에 진입했음을 알리고, 색상을 변경합니다.
            cameraSwitcher.SetPlayerInRange(true);
            if (instantiatedMaterial != null)
            {
                instantiatedMaterial.color = activeColor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 범위를 벗어나면 원래 색상으로 되돌립니다.
            cameraSwitcher.SetPlayerInRange(false);
            if (instantiatedMaterial != null)
            {
                instantiatedMaterial.color = defaultColor;
            }
        }
    }

    private void OnDestroy()
    {
        if (instantiatedMaterial != null)
        {
            Destroy(instantiatedMaterial);
        }
    }
}