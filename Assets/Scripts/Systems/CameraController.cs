using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using JetBrains.Annotations;
using Racimo;
using Unity.VisualScripting;
using Bouquet = Racimo.Bouquet;

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
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Count;
            EnableCurrentCamera();
        }
    }

    private void SwitchToPreviousCamera()
    {
        if (!lockedCamera)
        {
            DisableCurrentCamera();
            currentCameraIndex = (currentCameraIndex - 1 + cameras.Count) % cameras.Count;
            EnableCurrentCamera();
        }
    }

    public void EnableCurrentCamera()
    {
        if (cameras.Count > 0)
        {
            cameras[currentCameraIndex].GetVirtualCamera().gameObject.SetActive(true);

            workstationUI.Where(entry =>
                    entry.Equals(Bouquet.Instance.Workstation()) &&
                    GetCurrentCameraWorkstation() == Bouquet.Instance.Workstation()).ToList()
                .ForEach(entry => entry.EnableUI());
            workstationUI.Where(entry => !entry.Equals(Bouquet.Instance.Workstation())).ToList()
                .ForEach(entry => entry.DisableUI());
        }
    }

    private void DisableCurrentCamera()
    {
        if (cameras.Count > 0)
        {
            cameras[currentCameraIndex].GetVirtualCamera().gameObject.SetActive(false);
            workstationUI.ForEach(entry => entry.DisableUI());
        }
    }

    public bool SwitchToSpecificCamera(int id)
    {
        if (id >= cameras.Count)
        {
            //Debug.LogWarning("Specific camera not found, switching to next camera");
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
            //Debug.LogError("HUUHHHHH?????????");
            return false;
        }
    }

    public bool SwitchToSpecificCamera(Bouquet.Workstations workstation)
    {
        if (cameras.Count == 0)
        {
            //Debug.LogWarning("No cameras available");
            return false;
        }

        var cameraEntry = cameras.FirstOrDefault(entry => entry.Equals(workstation));
        if (cameraEntry != null)
        {
            int index = cameras.IndexOf(cameraEntry);
            SwitchToSpecificCamera(index);
            return false; 
        }
        else
        {
            //Debug.LogWarning("Camera not found for the specified workstation");
            return false;
        }
    }

    public int GetIDofWorkstation(Bouquet.Workstations workstation)
    {
        var cameraEntry = cameras.FirstOrDefault(entry => entry.Equals(workstation));
        if (cameraEntry != null)
        {
            return cameras.IndexOf(cameraEntry);
        }
        else
        {
            Debug.LogWarning("Camera not found for the specified workstation");
            return -1;
        }
    }
    private Bouquet.Workstations GetCurrentCameraWorkstation()
    {
        return cameras.First(entry => entry.GetVirtualCamera() == cameras[currentCameraIndex].GetVirtualCamera()).GetWorkstation();
    }

}