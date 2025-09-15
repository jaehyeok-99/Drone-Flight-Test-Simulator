using UnityEngine;
using Cinemachine;
using DroneController;
using System.Collections;
using TMPro;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCam;
    public Camera firstPersonCam;
    public GameObject player;
    public DroneMovement droneController;
    public PropellerMovement propellerMovement;
    public AudioController audioController;

    // 드론 사운드를 직접 제어하기 위한 AudioSource
    public AudioSource droneAudioSource;

    // 캐릭터 발소리를 직접 제어하기 위한 AudioSource
    public AudioSource characterFootstepAudio;

    // 드론 UI를 담고 있는 전체 캔버스를 참조합니다.
    public GameObject droneUICanvas;

    // 드론의 속도와 높이를 표시할 TextMeshProUGUI 변수
    public TextMeshProUGUI droneStatusText;

    private bool isThirdPerson = true;
    private bool isPlayerInRange = false;

    public float activationDelay = 1.0f;

    private Vector3 droneInitialPosition;
    private Quaternion droneInitialRotation;

    void Start()
    {
        if (droneController != null)
        {
            droneInitialPosition = droneController.transform.position;
            droneInitialRotation = droneController.transform.rotation;
        }
        SwitchMode();
    }

    void Update()
    {
        if ((isPlayerInRange || !isThirdPerson) && Input.GetKeyDown(KeyCode.T))
        {
            isThirdPerson = !isThirdPerson;
            SwitchMode();
        }

        // 드론 모드일 때만 속도와 고도 UI를 업데이트합니다.
        if (!isThirdPerson && droneController != null)
        {
            Rigidbody rb = droneController.GetComponent<Rigidbody>();
            if (rb != null && droneStatusText != null)
            {
                float currentSpeed = rb.linearVelocity.magnitude;
                float currentAltitude = droneController.transform.position.y;
                droneStatusText.text = $"Speed: {currentSpeed:F2} m/s\nAltitude: {currentAltitude:F2} m";
            }
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        isPlayerInRange = inRange;
    }

    void SwitchMode()
    {
        if (isThirdPerson)
        {
            // 플레이어 모드
            thirdPersonCam.gameObject.SetActive(true);
            firstPersonCam.gameObject.SetActive(false);
            player.GetComponent<StarterAssets.ThirdPersonController>().enabled = true;

            // 캐릭터 애니메이션 속도를 1로 되돌립니다.
            if (player.GetComponent<Animator>() != null)
            {
                player.GetComponent<Animator>().speed = 1;
            }

            if (droneUICanvas != null)
            {
                droneUICanvas.SetActive(false);
            }

            if (droneController != null)
            {
                droneController.enabled = false;
                droneController.transform.position = droneInitialPosition;
                droneController.transform.rotation = droneInitialRotation;
                Rigidbody rb = droneController.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            if (propellerMovement != null)
                propellerMovement.enabled = false;

            if (audioController != null)
            {
                audioController.enabled = false;
            }
            if (droneAudioSource != null && droneAudioSource.isPlaying)
            {
                droneAudioSource.Stop();
            }
        }
        else // 드론 모드로 전환될 때
        {
            // 드론 모드
            thirdPersonCam.gameObject.SetActive(false);
            firstPersonCam.gameObject.SetActive(true);
            player.GetComponent<StarterAssets.ThirdPersonController>().enabled = false;

            // 캐릭터 애니메이션과 소리를 즉시 멈춥니다.
            if (player.GetComponent<Animator>() != null)
            {
                player.GetComponent<Animator>().speed = 0;
            }
            if (characterFootstepAudio != null && characterFootstepAudio.isPlaying)
            {
                characterFootstepAudio.Stop();
            }

            if (droneUICanvas != null)
            {
                droneUICanvas.SetActive(true);
            }

            StartCoroutine(ActivateDroneScriptsWithDelay());
        }
    }

    IEnumerator ActivateDroneScriptsWithDelay()
    {
        if (droneController != null)
            droneController.enabled = false;
        if (propellerMovement != null)
            propellerMovement.enabled = false;
        if (audioController != null)
            audioController.enabled = false;

        if (droneAudioSource != null)
        {
            droneAudioSource.Stop();
        }

        yield return new WaitForSeconds(activationDelay);

        if (droneController != null)
            droneController.enabled = true;
        if (propellerMovement != null)
            propellerMovement.enabled = true;
        if (audioController != null)
            audioController.enabled = true;

        if (droneAudioSource != null)
        {
            droneAudioSource.Play();
        }
    }
}