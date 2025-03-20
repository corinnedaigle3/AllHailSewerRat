using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    public Camera ThirdPersonCamera;
    public Transform Player;  // Reference to the player character
    public float InteractionDistance = 3f;
    public GameObject interactionText;
    private InteractObject currentInteractable;

    void Update()
    {
        Ray r = new Ray(Player.position + Vector3.up, ThirdPersonCamera.transform.forward);
        Debug.DrawRay(Player.position + Vector3.up, ThirdPersonCamera.transform.forward, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(r, out hit, InteractionDistance))
        {
            InteractObject interactableObject = hit.collider.GetComponent<InteractObject>();
            if (interactableObject != null && interactableObject != currentInteractable)
            {
                currentInteractable = interactableObject;
                interactionText.SetActive(true);
                TextMeshProUGUI textComponent = interactionText.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = currentInteractable.GetInteractionText();
                }
            }
        }
        else
        {
            currentInteractable = null;
            interactionText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }
}