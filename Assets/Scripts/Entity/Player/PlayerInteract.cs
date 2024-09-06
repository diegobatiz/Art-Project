using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    public void Interact(InputAction.CallbackContext context)
    {
        Vector3 startPos = transform.position + (Vector3.forward * -5);
        Vector3 endPos = transform.position + (Vector3.forward * 5);

        RaycastHit2D raycast = Physics2D.Linecast(startPos, endPos, _layerMask);

        Debug.DrawLine(startPos, endPos, Color.magenta);

        if (raycast.collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
