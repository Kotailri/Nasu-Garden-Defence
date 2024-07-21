using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput = Vector2.zero;
    private Rigidbody2D RB;

    private bool MovementLocked = false;
    private float currentSlowMultiplier = 1f;

    // Clamp
    public Transform TopLeft;
    public Transform BotRight;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        minX = TopLeft.position.x;
        maxX = BotRight.position.x;
        minY = BotRight.position.y;
        maxY = TopLeft.position.y;
    }

    public void ApplySlow(float amount, float time)
    {
        currentSlowMultiplier = Mathf.Clamp(currentSlowMultiplier, 0f, 1f);
        amount = Mathf.Clamp(amount, 0f, 1f);
        amount += (amount * GlobalPlayer.GetStatValue(PlayerStatEnum.slowReduction));

        if (amount >= currentSlowMultiplier)
        {
            return;
        }

        StartCoroutine(ApplySlowCoroutine());

        IEnumerator ApplySlowCoroutine()
        {
            currentSlowMultiplier = amount;
            yield return new WaitForSeconds(time);
            currentSlowMultiplier = 1;
        }
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    public Vector2 GetVelocity()
    {
        return RB.velocity;
    }

    public void ToggleMoveInput(bool IsOn)
    {
        MovementLocked = !IsOn;
    }

    private void Update()
    {
        // Get axis x input
        float controllerInputX = 0;
        if (Mathf.Abs(Global.playerControls.GetAxisInputXLeft()) >= Config.ControllerDeadZone)
        {
            if (Global.playerControls.GetAxisInputXLeft() < 0) { controllerInputX = -1; }
            if (Global.playerControls.GetAxisInputXLeft() > 0) { controllerInputX = 1; }
        }

        // Get axis y input
        float controllerInputY = 0;
        if (Mathf.Abs(Global.playerControls.GetAxisInputYLeft()) >= Config.ControllerDeadZone)
        {
            if (Global.playerControls.GetAxisInputYLeft() < 0) { controllerInputY = -1; }
            if (Global.playerControls.GetAxisInputYLeft() > 0) { controllerInputY = 1; }
        }

        // Get keyboard input if controller not available
        float keyboardInputX = Global.playerControls.GetButtonRight() - Global.playerControls.GetButtonLeft();
        float keyboardInputY = Global.playerControls.GetButtonUp() - Global.playerControls.GetButtonDown();
        
        if (controllerInputX != 0 || controllerInputY != 0)
        {
            moveInput.x = controllerInputX;
            moveInput.y = controllerInputY;
        }
        else
        {
            moveInput.x = keyboardInputX;
            moveInput.y = keyboardInputY;
        }

        // Clamp to bounds
        if (moveInput.y > 0 && transform.position.y >= maxY)
        {
            moveInput.y = 0;
        }

        if (moveInput.y < 0 && transform.position.y <= minY)
        {
            moveInput.y = 0;
        }

        if (moveInput.x > 0 && transform.position.x >= maxX)
        {
            moveInput.x = 0;
        }

        if (Global.gameplayStarted)
        {
            if (moveInput.x < 0 && transform.position.x <= minX)
            {
                moveInput.x = 0;
            }
        }
        else
        {
            if (moveInput.x < 0 && transform.position.x <= -43.5f)
            {
                moveInput.x = 0;
            }
        }
        

        if (MovementLocked)
            moveInput = Vector2.zero;

        //print(GlobalPlayer.GetStatValue(PlayerStatEnum.movespeed));

        if (Global.gameplayStarted && Global.waveManager.IsWaveOngoing())
        {
            RB.velocity = moveInput.normalized * GlobalPlayer.GetStatValue(PlayerStatEnum.movespeed) * currentSlowMultiplier;
        }
        else
        {
            RB.velocity = moveInput.normalized * 12f;
        }
        

    }
}
