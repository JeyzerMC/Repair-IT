using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryboxBehaviour : MonoBehaviour, Interactee
{
    private bool isPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool OnInteraction(Interactror interactror)
    {
        if (!isPickedUp)
        {
            Debug.Log("I got picked up!");

            var hands = interactror.transform.Find("Hands");
            if (hands == null)
            {
                Debug.LogError("Interactor doesn't have \"Hands\"");
            }
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = hands.position;
            transform.parent = hands;

            // TODO: DISABLE THE RIGIDBODY COMPONENT (Disable != Delete)
            isPickedUp = true;
            return true;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        isPickedUp = false;
        transform.parent = null;

        return false;
    }
}
