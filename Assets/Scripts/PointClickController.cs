using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointClickController : PlayerControllerBase
{
    [Header("Point-Click Settings")]
    public float moveSpeed = 5f;
    public LayerMask groundLayer;

    private Vector3 targetPosition;
    private bool isMovingToTarget;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        UpdateCameraFollow();
    }

    void HandleMovement()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
            {
                targetPosition = hit.point;
                isMovingToTarget = true;
            }
        }

        if (!isMovingToTarget) return;

        Vector3 direction = targetPosition - transform.position;
        if (direction.sqrMagnitude > 0.01f)
        {
            direction.y = 0;
            controller.Move(direction.normalized * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(direction), 10f * Time.deltaTime);
        }
        else
        {
            isMovingToTarget = false;
        }
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        isMovingToTarget = false;
        targetPosition = transform.position;
    }
}