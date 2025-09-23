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

    public void RestoreState()
    {
        // 현재 상태(isThirdPerson)에 따라 모든 스크립트를 재활성화합니다.
        // 이렇게 하면 PauseMenu의 영향을 받지 않고 올바른 상태로 돌아갑니다.
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

        if (!isThirdPerson && Input.GetKeyDown(KeyCode.P))
        {
            if (droneController != null && droneController.enabled)
            {
                DeactivateDrone();
                if (activationMessageText != null)
                {
                    activationMessageText.gameObject.SetActive(false);
                }
            }
            else
            {
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