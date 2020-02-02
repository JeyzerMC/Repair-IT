using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchBehavior : ObjectContainer
{
    [SerializeField]
    Transform depotSpot;

    protected override bool TryAddObject(Takable gameObject, Interactror interactror)
    {
        if (containedObjects.Count != 0) {
            return false;
        }
        gameObject.transform.position = depotSpot.position;
        gameObject.transform.rotation = depotSpot.rotation;
        return true;
    }
}
