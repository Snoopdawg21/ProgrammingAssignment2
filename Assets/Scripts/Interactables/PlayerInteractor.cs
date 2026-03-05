using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputAction interactionInput;
    
    private IIInteractable interactable;
    private IIInteractable tempInteractable;

    void OnEnable()
    {
        interactionInput.Enable();
        interactionInput.performed += Interact;
    }

    void OnDisable()
    {
        interactionInput.performed -= Interact;
    }

    void OnTriggerEnter(Collider col)
    {
        tempInteractable = col?.gameObject.GetComponent<IIInteractable>();

        if (tempInteractable == null) return;
        
        interactable = tempInteractable;
        interactable?.OnHoverIn();
    }

    void OnTriggerExit(Collider col)
    {
        interactable?.OnHoverOut();
        
        if(tempInteractable == null)
            interactable = null;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        interactable?.OnInteract();
        Debug.Log("Interacted it");
    }
}
