using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using JetBrains.Annotations;
using Racimo;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    [Serializable]
    internal class WorkstationUIEntry : IEquatable<Bouquet.Workstations>
    {
        [SerializeField] private Bouquet.Workstations workstations;
        [SerializeField] [CanBeNull] private GameObject ui;

        public WorkstationUIEntry(Bouquet.Workstations workstations, GameObject ui)
        {
            this.workstations = workstations;
            this.ui = ui;
        }

        public GameObject GetUI() => ui;

        public void DisableUI()
        {
            if (ui != null) ui.SetActive(false);
        }

        public void EnableUI()
        {
            if (ui != null) ui.SetActive(true);
        }


        public bool Equals(Bouquet.Workstations other)
        {
            return workstations == other;
        }
    }

    [Serializable]
    private class CameraEntry
    {
        [SerializeField] private Bouquet.Workstations workstations;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public CameraEntry(Bouquet.Workstations workstations, CinemachineVirtualCamera virtualCamera)
        {
            this.workstations = workstations;
            this.virtualCamera = virtualCamera;
        }

        public CinemachineVirtualCamera GetVirtualCamera() => virtualCamera;
        public Bouquet.Workstations GetWorkstation() => workstations;

        public bool Equals(Bouquet.Workstations other)
        {
            return workstations == other;
        }
    }

    public List<CinemachineVirtualCamera> virtualCameras;
    public int currentCameraIndex = 1;
    public bool lockedCamera = false;
    public static CameraController instance;

    [SerializeField] private List<WorkstationUIEntry> workstationUI;
    [SerializeField] private List<CameraEntry> cameras;


    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Aseg�rate de que al menos una c�mara est� habilitada al inicio
        EnableCurrentCamera();
    }

    private void OnDestroy()
    {
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         // Cambia a la c�mara anterior en la lista
    //         GameManager.Trigger("OnCameraChange");
    //         SwitchToPreviousCamera();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         // Cambia a la c�mara siguiente en la lista
    //         GameManager.Trigger("OnCameraChange");
    //         SwitchToNextCamera();
    //     }
    // }

    private void SwitchToNextCamera()
    {
        if (!lockedCamera)
        {
            DisableCurrentCamera();
            currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Count;
            EnableCurrentCamera();
        }
    }

    private void SwitchToPreviousCamera()
    {
        if (!lockedCamera)
        {
            DisableCurrentCamera();
            currentCameraIndex = (currentCameraIndex - 1 + virtualCameras.Count) % virtualCameras.Count;
            EnableCurrentCamera();
        }
    }

    public void EnableCurrentCamera()
    {
        if (virtualCameras.Count > 0)
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(true);

            workstationUI.Where(entry => entry.Equals(Bouquet.Instance.Workstation()) && GetCurrentCameraWorkstation() == Bouquet.Instance.Workstation()).ToList()
                .ForEach(entry => entry.EnableUI());
            workstationUI.Where(entry => !entry.Equals(Bouquet.Instance.Workstation())).ToList()
                            .ForEach(entry => entry.DisableUI());
        }
    }

    private void DisableCurrentCamera()
    {
        if (virtualCameras.Count > 0)
        {
            virtualCameras[currentCameraIndex].gameObject.SetActive(false);
            workstationUI.ForEach(entry => entry.DisableUI());
        }
    }

    public bool SwitchToSpecificCamera(int id)
    {
        if (id >= virtualCameras.Count)
        {
            Debug.LogWarning("Specific camera not found, switching to next camera");
            SwitchToNextCamera();
            return false;
        }
        else if (currentCameraIndex != id && !lockedCamera)
        {
            GameManager.Trigger("OnCameraChange");
            DisableCurrentCamera();
            currentCameraIndex = id;
            EnableCurrentCamera();
            return false;
        }
        else if (currentCameraIndex == id)
        {
            return true;
        }
        else
        {
            Debug.LogError("HUUHHHHH?????????");
            return false;
        }
    }
    private Bouquet.Workstations GetCurrentCameraWorkstation()
    {
        return cameras.First(entry => entry.GetVirtualCamera() == virtualCameras[currentCameraIndex]).GetWorkstation();
    }
}