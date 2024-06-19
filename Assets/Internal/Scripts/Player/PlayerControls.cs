using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput controls;

    // Axis
    private float AxisInputXLeft;
    private float AxisInputYLeft;

    private float AxisInputXRight;
    private float AxisInputYRight;

    // Buttons
    private float ButtonUp;
    private float ButtonDown;
    private float ButtonLeft;
    private float ButtonRight;

    private void Awake()
    {
        Global.playerControls = this;
        controls = new PlayerInput();
        controls.GameplayControls.Enable();

        controls.GameplayControls.AxisXLeft.performed += ctx => AxisInputXLeft = ctx.ReadValue<float>();
        controls.GameplayControls.AxisYLeft.performed += ctx => AxisInputYLeft = ctx.ReadValue<float>();

        controls.GameplayControls.AxisXRight.performed += ctx => AxisInputXRight = ctx.ReadValue<float>();
        controls.GameplayControls.AxisYRight.performed += ctx => AxisInputYRight = ctx.ReadValue<float>();

        controls.GameplayControls.MoveLeft.performed += ctx => ButtonLeft = ctx.ReadValue<float>();
        controls.GameplayControls.MoveLeft.canceled += ctx => ButtonLeft = 0;

        controls.GameplayControls.MoveRight.performed += ctx => ButtonRight = ctx.ReadValue<float>();
        controls.GameplayControls.MoveRight.canceled += ctx => ButtonRight = 0;

        controls.GameplayControls.MoveUp.performed += ctx => ButtonUp = ctx.ReadValue<float>();
        controls.GameplayControls.MoveUp.canceled += ctx => ButtonUp = 0;

        controls.GameplayControls.MoveDown.performed += ctx => ButtonDown = ctx.ReadValue<float>();
        controls.GameplayControls.MoveDown.canceled += ctx => ButtonDown = 0;
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) 
            return;

        // interact functionality
        print("interact pressed");
    }

    public void ShootButtonPressed(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;

        // ring throw functionality
        print("shoot pressed");
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;

        // pause functionality
        print("pause pressed");
    }

    #region Axis Getters
    public float GetAxisInputXLeft()
    {
        return AxisInputXLeft;
    }

    public float GetAxisInputYLeft()
    {
        return AxisInputYLeft;
    }

    public float GetAxisInputXRight()
    {
        return AxisInputXRight;
    }

    public float GetAxisInputYRight()
    {
        return AxisInputYRight;
    }
    #endregion

    #region Movement Button Getters

    public float GetButtonUp()
    {
        return ButtonUp;
    }

    public float GetButtonDown()
    {
        return ButtonDown;
    }

    public float GetButtonLeft()
    {
        return ButtonLeft;
    }

    public float GetButtonRight()
    {
        return ButtonRight;
    }
    #endregion
}
