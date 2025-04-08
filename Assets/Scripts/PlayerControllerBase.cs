using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public abstract class PlayerControllerBase : MonoBehaviour
{
    [Header("Common References")]
    public CinemachineFreeLook freeLookCamera;
    protected CharacterController controller;
    
    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        ConfigureCameraSystem();
        CreateEventSystemIfNeeded();
    }

    void ConfigureCameraSystem()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            freeLookCamera.m_YAxis.m_InputAxisName = "";
        }
    }

    void CreateEventSystemIfNeeded()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
    }

    protected virtual void UpdateCameraFollow()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.Follow = transform;
            freeLookCamera.LookAt = transform;
        }
    }
}