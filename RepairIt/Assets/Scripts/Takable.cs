using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takable : Interactee
{
    private bool isPickedUp = false;

    public sealed override void OnInteraction(Interactror interactror)
    {
        if (interactror.IsHoldingObject && interactror.heldObject != this) {
            interactror.heldObject.OnInteraction(interactror);
            return;
        }
        isPickedUp = !isPickedUp;

        if (isPickedUp)
        {
            Debug.Log("I got picked up!");
            transform.position = interactror.Hands.position;
            transform.rotation = interactror.Hands.rotation;
        }

        transform.parent = isPickedUp ? interactror.Hands : null;
        interactror.heldObject = isPickedUp ? this : null;

        UpdateComponentStatus();
    }

    public void EnsurePlaced()
    {
        isPickedUp = true;
        UpdateComponentStatus();
    }

    void UpdateComponentStatus()
    {
        // TODO: indstead of resetting, set to previous state
        var colliders = GetComponents<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = !isPickedUp;
        }
        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = isPickedUp;
        }
    }
}
