using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> virtualCameras;
    public int currentCameraIndex = 1;
    public bool lockedCamera = false;
    public static CameraController instance;
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Aseg�rate de que al menos una c�mara est� habilitada al inicio
        EnableCurrentCamera();
        GameManager.Subscribe("SwitchToMessage", SwitchToNextCamera);
        GameManager.Subscribe("SwitchToWorkspace", SwitchToPreviousCamera);
    }
    private void OnDestroy(){
        GameManager.Unsuscribe("SwitchToMessage", SwitchToNextCamera);
        GameManager.Unsuscribe("SwitchToWorkspace", SwitchToPreviousCamera);
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
        if(!lockedCamera){
            DisableCurrentCamera();
            currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Count;
            EnableCurrentCamera();
        }
    }

    private void SwitchToPreviousCamera()
    {
        if(!lockedCamera){
            DisableCurrentCamera();
            currentCameraIndex = (currentCameraIndex - 1 + virtualCameras.Count) % virtualCameras.Count;
            EnableCurrentCamera();
        }
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
