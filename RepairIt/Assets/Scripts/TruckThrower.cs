using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckThrower : MonoBehaviour
{
    [SerializeField]
    Transform throwingPosition = null;
    [SerializeField]
    Vector3 force = new Vector3(0, 0, 9.81f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Rigidbody ThrowBox(Rigidbody box)
    {
        Rigidbody newBox = Instantiate(box, throwingPosition.position, throwingPosition.rotation);
        newBox.AddRelativeForce(force, ForceMode.Impulse);
        return newBox;
    }
}
