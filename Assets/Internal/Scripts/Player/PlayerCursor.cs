using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody2D RB;

    private void Awake()
    {
        Global.cursorTransform = transform;
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get axis x input
        float controllerInputX = 0;
        if (Mathf.Abs(Global.playerControls.GetAxisInputXRight()) >= Config.ControllerDeadZone)
        {
            if (Global.playerControls.GetAxisInputXRight() < 0) { controllerInputX = -1; }
            if (Global.playerControls.GetAxisInputXRight() > 0) { controllerInputX = 1; }
        }

        // Get axis y input
        float controllerInputY = 0;
        if (Mathf.Abs(Global.playerControls.GetAxisInputYRight()) >= Config.ControllerDeadZone)
        {
            if (Global.playerControls.GetAxisInputYRight() < 0) { controllerInputY = -1; }
            if (Global.playerControls.GetAxisInputYRight() > 0) { controllerInputY = 1; }
        }

        if (!IsCursorOnScreen())
        {
            moveInput.x = controllerInputX;
            moveInput.y = controllerInputY;

            if (moveInput == Vector2.zero)
                transform.position = Global.playerTransform.position;

            RB.velocity = moveInput.normalized * Config.CursorSpeed;
        }
        else
        {
            // Get the position of the cursor in screen space
            Vector3 cursorScreenPosition = Input.mousePosition;

            // Convert the screen position to world space
            Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);

            // Since ScreenToWorldPoint gives a position based on the camera's z-depth,
            // ensure the z coordinate of cursorWorldPosition matches the z coordinate of the object.
            cursorWorldPosition.z = transform.position.z;

            // Update the position of the object to follow the cursor
            transform.position = cursorWorldPosition;
        }
    }

    private bool IsCursorOnScreen()
    {
        #if UNITY_EDITOR
        if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 1 || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 1)
        {
            return false;
        }
        #else
        if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 || Input.mousePosition.x >= Screen.width - 1 || Input.mousePosition.y >= Screen.height - 1) {
            return false;
        }
        #endif
        else
        {
            return true;
        }
    }
}
