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
            return TryCraft(gameObject);
        }
        gameObject.transform.position = depotSpot.position;
        gameObject.transform.rotation = depotSpot.rotation;
        return true;
    }

    // Returns if we consumed the object with our action
    protected bool TryCraft(Takable obj)
    {
        if (containedObjects.Count != 1)
        {
            return false;
        }
        if (obj.CompareTag("Exacto") && containedObjects[0].TryGetComponent<DeliveryboxBehaviour>(out var deliveryBox))
        {
            var content = Instantiate(deliveryBox.content, depotSpot.position, depotSpot.rotation, transform);
            content.EnsurePlaced();
            containedObjects[0] = content;
            Destroy(deliveryBox.gameObject); // No more box
            return false; // We don't consume the exacto, it was only used to open the box.
        }
        if (containedObjects[0].TryGetComponent<Repairable>(out var repairable))
        {
            int index = repairable.requirements.FindIndex(obj.CompareTag);
            if (index == -1)
            {
                // not part of requirements
                return false;
            }
            repairable.requirements.RemoveAt(index);
            Destroy(obj.gameObject);
            return true;
        }
        return false;
    }

    protected override void PostPutHook()
    {
        if (containedObjects.Count > 1)
        {
            // We just crafted something, remove all but index 0
            containedObjects.RemoveRange(1, containedObjects.Count - 1);
        }
    }
}
