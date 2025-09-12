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
            Debug.LogError("������ CameraSwitcher ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    // Ʈ���� �ȿ� �ӹ��� ���� ��� ȣ��˴ϴ�.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ���� ������ ���������� �˸���, ������ �����մϴ�.
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
            // �÷��̾ ������ ����� ���� �������� �ǵ����ϴ�.
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