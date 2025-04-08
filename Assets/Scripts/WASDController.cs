using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WASDController : PlayerControllerBase
{
    [Header("WASD Settings")]
    public float moveSpeed = 5f;
    public float mouseXSensitivity = 0.5f;
    public float mouseYSensitivity = 0.3f;

    private Vector2 mouseDelta;

    Transform camTransform;
    
    void Awake()
    {
        camTransform = Camera.main?.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
        UpdateCameraFollow();
    }

    void HandleMovement()
    {
        Vector2 input = new Vector2(
            Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(),
            Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue()
        );

        Vector3 camForward = camTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = camTransform.right;
        camRight.y = 0;
        camRight.Normalize();
        
        Vector3 movement = (camRight * input.x + camForward * input.y);
        movement = Vector3.ClampMagnitude(movement, 1); 
        
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    void HandleCamera()
    {
        mouseDelta = Mouse.current.delta.ReadValue();
        freeLookCamera.m_XAxis.Value += mouseDelta.x * mouseXSensitivity;
        freeLookCamera.m_YAxis.Value -= mouseDelta.y * mouseYSensitivity;
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}