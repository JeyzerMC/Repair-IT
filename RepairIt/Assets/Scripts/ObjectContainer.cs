using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectContainer : MonoBehaviour, Interactee
{
    protected List<GameObject> containedObjects = new List<GameObject>();

    protected abstract bool TryAddObject(GameObject gameObject);

    /// <summary>
    /// DO NOT OVERRIDE THIS METHOD
    /// Adds a GameObject to the object container
    /// </summary>
    /// <param name="gameObject"></param>
    public bool PutObject(GameObject interactee)
    {
        if (TryAddObject(interactee))
        {
            Debug.Log("Object ADDED to container!");
            containedObjects.Add(interactee);
            return true;
        }

        return false;
    }

    public abstract bool OnInteraction(Interactror interactror);
}
