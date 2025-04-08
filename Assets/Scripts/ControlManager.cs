using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public WASDController wasdController;
    public PointClickController pointClickController;
    public UISticksController uiSticksController;

    void Update()
    {
        HandleControlSwitch();
    }

    void HandleControlSwitch()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SetActiveController(wasdController);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SetActiveController(pointClickController);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SetActiveController(uiSticksController);
        }
    }

    void SetActiveController(PlayerControllerBase activeController)
    {
        wasdController.enabled = activeController == wasdController;
        pointClickController.enabled = activeController == pointClickController;
        uiSticksController.enabled = activeController == uiSticksController;
    }
}