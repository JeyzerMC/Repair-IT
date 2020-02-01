﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("key_Vertical") * speed;
        float rotation = Input.GetAxis("key_Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Riz publicité");
            Debug.DrawLine(
                transform.position,
                transform.position + 30.0f * transform.forward,
                Color.white,
                5.0f
            );
        }
    }
}
