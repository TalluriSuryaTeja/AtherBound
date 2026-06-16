using UnityEngine;
using UnityEngine.InputSystem; // Added to support the new Input System

public class PlayerController : MonoBehaviour
{
    public enum PlayerID { Player1, Player2 }
    
    [Header("Player Settings")]
    public PlayerID playerId;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 1.1f;

    private float currentSpeed;
    private Vector3 currentMoveDirection;

    [Header("Interaction Settings")]
    public float interactRange = 2f;

    // Components
    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); // Grabs the animator on the player or the child model
        
        if (rb != null)
        {
            // Lock rotation so physics collisions never tip the character over!
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJump();
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        if (rb != null && currentMoveDirection != Vector3.zero)
        {
            // MovePosition is physics-safe! It perfectly slides along walls instead of teleporting through them.
            rb.MovePosition(rb.position + currentMoveDirection * currentSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleMovementInput()
    {
        Vector3 moveInput = Vector3.zero;
        bool isRunning = false;

        if (playerId == PlayerID.Player1)
        {
            // WASD for Player 1 using the New Input System
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) moveInput.z = 1;
                if (Keyboard.current.sKey.isPressed) moveInput.z = -1;
                if (Keyboard.current.aKey.isPressed) moveInput.x = -1;
                if (Keyboard.current.dKey.isPressed) moveInput.x = 1;
                
                // Left Shift to Sprint
                if (Keyboard.current.leftShiftKey.isPressed) isRunning = true;
            }
        }
        else if (playerId == PlayerID.Player2)
        {
            // Arrows for Player 2
            if (Keyboard.current != null)
            {
                if (Keyboard.current.upArrowKey.isPressed) moveInput.z = 1;
                if (Keyboard.current.downArrowKey.isPressed) moveInput.z = -1;
                if (Keyboard.current.leftArrowKey.isPressed) moveInput.x = -1;
                if (Keyboard.current.rightArrowKey.isPressed) moveInput.x = 1;
                
                // Right Ctrl to Sprint
                if (Keyboard.current.rightCtrlKey.isPressed) isRunning = true;
            }

            // Controller support (Left Joystick)
            if (Gamepad.current != null)
            {
                Vector2 stick = Gamepad.current.leftStick.ReadValue();
                if (moveInput == Vector3.zero && stick.sqrMagnitude > 0.01f)
                {
                    moveInput.x = stick.x;
                    moveInput.z = stick.y;
                }
                
                // Triggers to Sprint
                if (Gamepad.current.rightTrigger.isPressed || Gamepad.current.leftTrigger.isPressed) isRunning = true;
            }
        }

        // Normalize to prevent faster diagonal movement
        if (moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }

        // Update speed state
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Apply the slope math trick and store it for FixedUpdate physics
        currentMoveDirection = GetSlopeMoveDirection(moveInput);

        // Rotate to face movement direction smoothly
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Send movement speed to the Animator (Walk = 0.5, Run = 1.0)
        if (animator != null)
        {
            float animSpeed = moveInput.magnitude * (isRunning ? 1f : 0.5f);
            animator.SetFloat("Speed", animSpeed);
        }
    }

    private Vector3 GetSlopeMoveDirection(Vector3 moveDirection)
    {
        // Don't calculate if we aren't trying to move
        if (moveDirection == Vector3.zero) return Vector3.zero;

        RaycastHit hit;
        // Shoot laser to find ground
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, groundCheckDistance))
        {
            // Bend the horizontal movement to perfectly match the slant of the hill
            return Vector3.ProjectOnPlane(moveDirection, hit.normal).normalized * moveDirection.magnitude;
        }
        
        // If we are in the air, just move normally
        return moveDirection;
    }

    private void HandleInteraction()
    {
        bool interactPressed = false;

        if (playerId == PlayerID.Player1)
        {
            // E to interact
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactPressed = true;
            }
        }
        else if (playerId == PlayerID.Player2)
        {
            // M to interact
            if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
            {
                interactPressed = true;
            }
            
            // Controller face button down (usually 'A' on Xbox)
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                interactPressed = true;
            }
        }

        if (interactPressed)
        {
            Interact();
        }
    }

    private void HandleJump()
    {
        bool jumpPressed = false;

        if (playerId == PlayerID.Player1)
        {
            // Spacebar for Player 1
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                jumpPressed = true;
            }
        }
        else if (playerId == PlayerID.Player2)
        {
            // Right Shift for Player 2
            if (Keyboard.current != null && Keyboard.current.rightShiftKey.wasPressedThisFrame)
            {
                jumpPressed = true;
            }
            
            // Controller East button (B on Xbox / Circle on PlayStation)
            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                jumpPressed = true;
            }
        }

        if (jumpPressed && IsGrounded() && rb != null)
        {
            // Reset vertical velocity for consistent jump height
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // Simple raycast downwards to check if we are standing on something
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance);
    }

    private void Interact()
    {
        // Find all colliders within interactRange
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRange);
        
        ResourceNode closestNode = null;
        float closestDistance = float.MaxValue;

        // Find the closest resource node
        foreach (var hitCollider in hitColliders)
        {
            ResourceNode node = hitCollider.GetComponent<ResourceNode>();
            if (node != null)
            {
                float distance = Vector3.Distance(transform.position, node.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNode = node;
                }
            }
        }

        // Gather from the closest node if we found one
        if (closestNode != null)
        {
            closestNode.Gather(playerId);
        }
        else
        {
            Debug.Log(playerId + " found nothing to interact with.");
        }
    }
}
