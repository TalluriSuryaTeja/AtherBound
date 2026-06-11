using UnityEngine;
using UnityEngine.InputSystem; // Added to support the new Input System

public class PlayerController : MonoBehaviour
{
    public enum PlayerID { Player1, Player2 }
    
    [Header("Player Settings")]
    public PlayerID playerId;
    public float moveSpeed = 5f;

    [Header("Interaction Settings")]
    public float interactRange = 2f;

    // Optional component for physics-based movement later
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        Vector3 moveInput = Vector3.zero;

        if (playerId == PlayerID.Player1)
        {
            // WASD for Player 1 using the New Input System
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) moveInput.z = 1;
                if (Keyboard.current.sKey.isPressed) moveInput.z = -1;
                if (Keyboard.current.aKey.isPressed) moveInput.x = -1;
                if (Keyboard.current.dKey.isPressed) moveInput.x = 1;
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
            }
        }

        // Normalize to prevent faster diagonal movement
        if (moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }

        // Move the player (kinematic movement)
        transform.Translate(moveInput * moveSpeed * Time.deltaTime, Space.World);

        // Rotate to face movement direction smoothly
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
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
