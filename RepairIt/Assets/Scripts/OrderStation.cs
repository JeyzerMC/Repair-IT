using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStation : Interactee
{
    [SerializeField]
    DeliveryboxBehaviour boxPrefab = null;
    TruckThrower _truckThrower;

    [SerializeField]
    Takable content;

    // Start is called before the first frame update
    void Start()
    {
        // Lets find the truck thrower
        _truckThrower = GameObject.FindObjectOfType<TruckThrower>();
    }

    public override void OnInteraction(Interactror interactror)
    {
        Debug.Log("Bruh");
        _truckThrower.ThrowBox(boxPrefab.GetComponent<Rigidbody>()).GetComponent<DeliveryboxBehaviour>().content = content;
    }
}
