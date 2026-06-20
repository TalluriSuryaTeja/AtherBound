using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;      // For targeted raycast
    public float lootRadius = 0.5f;             // For area-of-effect loot detection
    public LayerMask interactionLayer;
    public Text interactionPromptText;
    public Camera mainCamera;

    private Interactable focusedInteractable;   // The item targeted by the raycast
    private List<Interactable> nearbyInteractables = new List<Interactable>();
    private int nearbyCurrentIndex = 0;

    void Start()
    {
        if (interactionPromptText != null) interactionPromptText.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckForFocusedInteractable();
        FindNearbyLoot();
        HandleInput();
        UpdatePrompt();
    }

    private void CheckForFocusedInteractable()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                focusedInteractable = interactable;
                return;
            }
        }
        focusedInteractable = null;
    }

    private void FindNearbyLoot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lootRadius, interactionLayer);
        nearbyInteractables.Clear();

        foreach (var collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                nearbyInteractables.Add(interactable);
            }
        }

        if (nearbyInteractables.Count > 0)
        {
            nearbyInteractables = nearbyInteractables.OrderBy(i => Vector3.Distance(transform.position, i.transform.position)).ToList();
        }

        if (nearbyCurrentIndex >= nearbyInteractables.Count)
        {
            nearbyCurrentIndex = 0;
        }
    }

    private void HandleInput()
    {
        // Scroll through nearby loot
        if (nearbyInteractables.Count > 1 && focusedInteractable == null) // Allow scroll only when not targeting something specific
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) nearbyCurrentIndex = (nearbyCurrentIndex - 1 + nearbyInteractables.Count) % nearbyInteractables.Count;
            else if (scroll < 0f) nearbyCurrentIndex = (nearbyCurrentIndex + 1) % nearbyInteractables.Count;
        }

        // Primary interaction with 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Prioritize the focused interactable
            if (focusedInteractable != null)
            {
                focusedInteractable.BaseInteract();
            }
            // Otherwise, interact with the closest item in the loot list
            else if (nearbyInteractables.Count > 0)
            {
                nearbyInteractables[nearbyCurrentIndex].BaseInteract();
            }
        }

        // "Pick All" with 'F'
        if (Input.GetKeyDown(KeyCode.F) && CanPickAll())
        {
            PickAll();
        }
    }

    private void UpdatePrompt()
    {
        // If we are looking at a specific interactable from far away
        if (focusedInteractable != null)
        {
            interactionPromptText.text = $"[E] {focusedInteractable.promptMessage}";
            interactionPromptText.gameObject.SetActive(true);
        }
        // If we are standing on a pile of loot
        else if (nearbyInteractables.Count > 0)
        {
            string prompt = $"[E] {nearbyInteractables[nearbyCurrentIndex].promptMessage}";

            if (nearbyInteractables.Count > 1)
            {
                prompt += $" (Scroll to cycle {nearbyCurrentIndex + 1}/{nearbyInteractables.Count})";
            }

            if (CanPickAll())
            {
                prompt += "\n[F] Pick All";
            }

            interactionPromptText.text = prompt;
            interactionPromptText.gameObject.SetActive(true);
        }
        // If there's nothing to interact with
        else
        {
            interactionPromptText.gameObject.SetActive(false);
        }
    }

    private bool CanPickAll()
    {
        return nearbyInteractables.Count > 1 && nearbyInteractables.All(i => i is ItemPickup);
    }

    private void PickAll()
    {
        List<Interactable> itemsToPick = new List<Interactable>(nearbyInteractables);
        foreach (var item in itemsToPick)
        {
            item.BaseInteract();
        }
        nearbyInteractables.Clear();
        nearbyCurrentIndex = 0;
    }
}
