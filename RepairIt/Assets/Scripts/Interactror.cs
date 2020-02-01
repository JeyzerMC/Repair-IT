using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Interactror : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 0.9f;

    private PlayerInput input;
    private GameObject heldObject = null;

    public bool IsHoldingObject { get { return heldObject != null; } private set { } }

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetPlayerButtonDown("Fire1"))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Debug.Log("Trying to interact");
        bool foundContainer = false;
        GameObject foundInteractee = null;

        foreach (var hit in Physics.RaycastAll(transform.position, transform.forward, MAX_RAYCAST_DISTANCE).OrderBy(x => x.distance))
        {
            Debug.Log("Testing hit at distance: "+hit.distance);
            var collider = hit.collider;
            if (collider.TryGetComponent<Interactee>(out var interactee))
            {
                if(IsHoldingObject && collider.TryGetComponent<ObjectContainer>(out var container))
                {
                    Debug.Log("INTERACTING WITH CONTAINER!!!");
                    if (container.PutObject(heldObject))
                    {
                        heldObject = null;
                    }
                    foundContainer = true;
                    break;
                }

                if (foundInteractee == null) {
                    foundInteractee = collider.gameObject;
                }
            }
        }

        if(!foundContainer && foundInteractee != null)
        {
            Debug.Log("Didn't find container, interacting with the interactee!");
            InteractWith(foundInteractee);
        }
    }

    public void InteractWith(GameObject gameObject)
    {
        if(!gameObject.TryGetComponent<Interactee>(out var interactee))
        {
            return;
        }

        if (interactee.OnInteraction(this))
        {
            heldObject = gameObject;
        }
    }
}
