using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> virtualCameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        // Aseg�rate de que al menos una c�mara est� habilitada al inicio
        EnableCurrentCamera();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Cambia a la c�mara anterior en la lista
            SwitchToPreviousCamera();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Cambia a la c�mara siguiente en la lista
            SwitchToNextCamera();
        }
    }

    private void SwitchToNextCamera()
    {
        DisableCurrentCamera();
        currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Count;
        EnableCurrentCamera();
    }

    private void SwitchToPreviousCamera()
    {
        DisableCurrentCamera();
        currentCameraIndex = (currentCameraIndex - 1 + virtualCameras.Count) % virtualCameras.Count;
        EnableCurrentCamera();
    }

    private void EnableCurrentCamera()
    {
        if (virtualCameras.Count > 0)
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }

    private void DisableCurrentCamera()
    {
        if (virtualCameras.Count > 0)
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
        }
    }
}
