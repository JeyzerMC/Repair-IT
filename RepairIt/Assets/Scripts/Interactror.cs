using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Interactror : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 1.2f;

    private PlayerInput input;
    private GameObject heldObject = null;
    private Transform Hands;

    public bool IsHoldingObject { get { return heldObject != null; } private set { } }

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        Hands = transform.Find("Hands");
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetPlayerButtonDown("Fire1"))
        {
            TryInteract();
        }

        Debug.DrawRay(Hands.position, Quaternion.AngleAxis(65f, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE, Color.red, 0, false);
    }

    void TryInteract()
    {
        Debug.Log("Trying to interact");
        bool foundContainer = false;
        GameObject foundInteractee = null;

        foreach (var hit in Physics.RaycastAll(Hands.position, Quaternion.AngleAxis(65f, Hands.right) * Hands.forward, MAX_RAYCAST_DISTANCE).OrderBy(x => x.distance))
        {
            Debug.Log("Testing hit at distance: " + hit.distance);
            var collider = hit.collider;
            if (collider.TryGetComponent<Interactee>(out var interactee))
            {
                Debug.Log("FOUND INTERACTEE!");
                if (IsHoldingObject && collider.TryGetComponent<ObjectContainer>(out var container))
                {
                    Debug.Log("INTERACTING WITH CONTAINER!!!");
                    if (container.PutObject(heldObject))
                    {
                        heldObject = null;
                    }
                    foundContainer = true;
                    break;
                }

                if (foundInteractee == null)
                {
                    foundInteractee = collider.gameObject;
                }
            }
        }

        if (!foundContainer && foundInteractee != null)
        {
            Debug.Log("Didn't find container, interacting with the interactee!");
            InteractWith(foundInteractee);
            return;
        }

        if (heldObject != null)
        {
            InteractWith(heldObject);
        }
    }

    public void InteractWith(GameObject gameObject)
    {
        if (!gameObject.TryGetComponent<Interactee>(out var interactee))
        {
            return;
        }

        if (interactee.OnInteraction(this))
        {
            heldObject = gameObject;
        }
        else
        {
            heldObject = null;
        }
    }
}
