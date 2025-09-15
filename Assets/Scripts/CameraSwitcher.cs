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

    public AudioSource droneAudioSource;
    public AudioSource characterFootstepAudio;

    public GameObject droneUICanvas;
    public TextMeshProUGUI droneStatusText;

    public TextMeshProUGUI activationMessageText;

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

        // 특정 키(P)를 누르면 드론 시동/정지
        if (!isThirdPerson && Input.GetKeyDown(KeyCode.P))
        {
            // 드론이 현재 활성화 상태면, 비활성화합니다.
            if (droneController != null && droneController.enabled)
            {
                DeactivateDrone();

                // 드론이 꺼졌을 때는 다시 활성화 메시지가 나타나지 않게 합니다.
                if (activationMessageText != null)
                {
                    activationMessageText.gameObject.SetActive(false);
                }
            }
            // 드론이 비활성화 상태면, 시동을 겁니다.
            else
            {
                // P키를 누르면 깜빡임을 멈추고 메시지를 비활성화합니다.
                StopCoroutine("BlinkText");
                if (activationMessageText != null)
                {
                    activationMessageText.gameObject.SetActive(false);
                }
                StartCoroutine(ActivateDroneScriptsWithDelay());
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
            thirdPersonCam.gameObject.SetActive(true);
            firstPersonCam.gameObject.SetActive(false);
            player.GetComponent<StarterAssets.ThirdPersonController>().enabled = true;

            if (player.GetComponent<Animator>() != null)
            {
                player.GetComponent<Animator>().speed = 1;
            }

            if (droneUICanvas != null)
            {
                droneUICanvas.SetActive(false);
            }

            DeactivateDrone();
            StopCoroutine("BlinkText");
        }
        else
        {
            thirdPersonCam.gameObject.SetActive(false);
            firstPersonCam.gameObject.SetActive(true);
            player.GetComponent<StarterAssets.ThirdPersonController>().enabled = false;

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

            if (activationMessageText != null)
            {
                activationMessageText.gameObject.SetActive(true);
                StartCoroutine("BlinkText");
            }
        }
    }

    void DeactivateDrone()
    {
        if (droneController != null)
            droneController.enabled = false;
        if (propellerMovement != null)
            propellerMovement.enabled = false;
        if (audioController != null)
            audioController.enabled = false;

        if (droneAudioSource != null && droneAudioSource.isPlaying)
        {
            droneAudioSource.Stop();
        }
    }

    IEnumerator ActivateDroneScriptsWithDelay()
    {
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

    IEnumerator BlinkText()
    {
        while (true)
        {
            activationMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            activationMessageText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}