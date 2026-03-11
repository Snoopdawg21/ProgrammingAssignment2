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

    void OnTriggerStay(Collider col)
    {
        tempInteractable = col?.gameObject.GetComponent<IIInteractable>();

        if (tempInteractable == null) return;
        
        interactable = tempInteractable;
        interactable?.OnHoverIn();
        
        Debug.Log(col.gameObject.name);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<IIInteractable>() == null) return;
        
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
