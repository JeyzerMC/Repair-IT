using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class WarpOnMove : MonoBehaviour
{

    PlayerInput _input;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
