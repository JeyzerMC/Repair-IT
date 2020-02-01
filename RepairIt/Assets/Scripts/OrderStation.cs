using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStation : MonoBehaviour, Interactee
{
    [SerializeField]
    Rigidbody boxPrefab = null;
    TruckThrower _truckThrower;

    // Start is called before the first frame update
    void Start()
    {
        // Lets find the truck thrower
        _truckThrower = GameObject.FindObjectOfType<TruckThrower>();
    }
    
    public void OnInteraction(Interactror interactror)
    {
        Debug.Log("Bruh");
        _truckThrower.ThrowBox(boxPrefab);
    }

}
