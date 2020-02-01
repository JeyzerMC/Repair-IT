using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactror : MonoBehaviour
{
    public const float MAX_RAYCAST_DISTANCE = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Debug.Log("Trying to interact");
        foreach (var hit in Physics.RaycastAll(transform.position, transform.forward, MAX_RAYCAST_DISTANCE).OrderBy(x => x.distance))
        {
            Debug.Log("Testing hit at distance: "+hit.distance);
            if (hit.collider.TryGetComponent<Interactee>(out var interactee))
            {
                interactee.OnInteraction(this);
                break;
            }
        }
        
    }
}
