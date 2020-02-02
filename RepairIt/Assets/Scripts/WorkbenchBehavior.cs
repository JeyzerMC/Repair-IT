using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchBehavior : ObjectContainer
{

    [SerializeField]
    Transform depotSpot;

    protected override bool TryAddObject(Takable gameObject)
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
        if (obj.CompareTag("Exacto") && containedObjects.Count == 1 && containedObjects[0].TryGetComponent<DeliveryboxBehaviour>(out var deliveryBox))
        {
            var content = Instantiate(deliveryBox.content, depotSpot.position, depotSpot.rotation, transform);
            content.EnsurePlaced();
            containedObjects[0] = content;
            Destroy(deliveryBox.gameObject); // No more box
            return false; // We don't consume the exacto, it was only used to open the box.
        }
        return false;
    }
}
