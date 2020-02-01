using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Interactror : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 1.2f;

    private PlayerInput input;
    private GameObject heldObject = null;
    private Transform Hands;

    public float minWhiskerAngle = 65;
    public float maxWhiskerAngle = 90;
    public int whiskerNumber = 5;

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
    }

    void TryInteract()
    {
        Debug.Log("Trying to interact");
        bool foundContainer = false;
        GameObject foundInteractee = null;

        var hits = RaycastWhiskers(Hands.position,
            Quaternion.AngleAxis(minWhiskerAngle, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE,
            Quaternion.AngleAxis(maxWhiskerAngle, Hands.right) * Hands.forward * MAX_RAYCAST_DISTANCE,
            whiskerNumber);

        foreach (var hit in hits)
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

        if (!IsHoldingObject && !foundContainer && foundInteractee != null)
        {
            Debug.Log("Didn't find container, interacting with the interactee!");
            InteractWith(foundInteractee);
            return;
        }

        if (IsHoldingObject)
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

    public IEnumerable<RaycastHit> RaycastWhiskers(Vector3 position, Vector3 fromDirection, Vector3 toDirection, int number)
    {
        List<RaycastHit> hits = new List<RaycastHit>();

        for (int i = 0; i < number; ++i)
        {
            float progress = i / ((float)number - 1);
            Vector3 direction = Vector3.Slerp(fromDirection, toDirection, progress);
            Debug.DrawRay(
                position,
                direction,
                Color.Lerp(Color.red, Color.white, progress),
                1, // 1 second display for debugging
                false);
            var hit = Physics.RaycastAll(position, direction, direction.magnitude).OrderBy(x => x.distance);
            hits.AddRange(hit);
        }

        return hits;
    }
}
