using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectContainer : Interactee
{
    /// <summary>
    /// Last element will be taken
    /// </summary>
    protected List<Takable> containedObjects = new List<Takable>();

    protected abstract bool TryAddObject(Takable gameObject, Interactror interactror);

    /// <summary>
    /// DO NOT OVERRIDE THIS METHOD
    /// Adds a GameObject to the object container
    /// </summary>
    /// <param name="gameObject"></param>
    public bool PutObject(Takable interactee, Interactror interactror)
    {
        if (TryAddObject(interactee, interactror))
        {
            Debug.Log("Object ADDED to container!");
            containedObjects.Add(interactee);
            return true;
        }

        return false;
    }

    public sealed override void OnInteraction(Interactror interactror)
    {
        if (interactror.IsHoldingObject)
        {
            if (PutObject(interactror.heldObject, interactror))
            {
                interactror.heldObject.transform.parent = transform;
                interactror.heldObject = null;
                OnInteractionImpl(interactror);
            }
        }
        else
        {
            if (containedObjects.Count != 0)
            {
                interactror.heldObject = containedObjects[containedObjects.Count - 1];
                containedObjects.RemoveAt(containedObjects.Count - 1);
                interactror.heldObject.transform.position = interactror.Hands.position;
                interactror.heldObject.transform.parent = interactror.Hands;
                OnInteractionImpl(interactror);
            }
        }
        
    }

    protected virtual void OnInteractionImpl(Interactror interactror) {}
}
